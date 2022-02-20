using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Commands;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Business.Entities.Enums.Combat;
using MarrowVale.Common.Contracts;
using MarrowVale.Common.Prompts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarrowVale.Business.Services
{
    public class CombatService : ICombatService
    {
        private readonly ILogger<CombatService> _logger;
        private readonly IPrintService _printService;
        private readonly INpcRepository _npcRepository;
        private readonly IPromptService _promptService;
        private readonly IAiService _aiService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IDialogueService _dialogueService;
        private readonly IDivineInterventionService _divineInterventionService;

        public string Name { get; set; }
        public CombatService(ILoggerFactory loggerFactory, IPrintService printService, INpcRepository npcRepository, IPromptService promptService, 
            IAiService aiService, IPlayerRepository playerRepository, IDialogueService dialogueService, IDivineInterventionService divineInterventionService)
        {
            _logger = loggerFactory.CreateLogger<CombatService>();
            _printService = printService;
            _npcRepository = npcRepository;
            _promptService = promptService;
            _aiService = aiService;
            _playerRepository = playerRepository;
            _dialogueService = dialogueService;
            _divineInterventionService = divineInterventionService;
        }

        public string Attack(Player player, Npc npc)
        {            
            var enemies = new List<Npc>();
            enemies.Add(npc);

            //
            //var otherNpcReactions = _npcActionService();

            while (player.IsAlive() && enemies.Any(x => x.IsAlive))
            {
                var (playerAction, text) = getPlayerAction(player);

                if (!playersTurn(playerAction, player, enemies, text))
                    return "";

                foreach (var enemy in enemies)
                {
                    if (!enemy.IsAlive || !player.IsAlive())
                        continue;
                    enemiesTurn(player, enemy);
                }
                enemies = enemies.Where(x => x.IsAlive).ToList();

            }

            return "Fight has concluded";
        }


        private (CombatActionEnum, string) getPlayerAction(Player player)
        {
            var isActionValid = false;
            var text = "";
            var intendedAction = CombatActionEnum.Unknown;
            while (!isActionValid)
            {
                (intendedAction, text) = determineIntendedAction();
                isActionValid = validateAction(intendedAction, player);

                if (!isActionValid)
                    _printService.Print("Invalid Action");
            }

            Console.WriteLine();
            return (intendedAction, text);
        }

        private bool playersTurn(CombatActionEnum action, Player player, List<Npc> enemies, string text) =>
            action switch
            {
                CombatActionEnum.Melee => meleeAttack(player, enemies, text),
                CombatActionEnum.Spell => spellAttack(player, enemies, text),
                CombatActionEnum.Flee => flee(player, enemies, text),
                CombatActionEnum.DivineHelp => divineHelp(player, enemies, text),
                CombatActionEnum.Talk => talk(player, enemies, text),
                CombatActionEnum.PickUpItem => pickupItem(player, enemies, text),
                _ => flee(player, enemies, text)
            };

        private void enemiesTurn(Player player, Npc enemy)
        {
            var weapon = enemy.Weapon;
            var damage = calculateDamage(player?.CurrentArmor, weapon);

            player.CurrentHealth -= damage;

            Console.WriteLine();
            if (player.CurrentHealth <= 0)
            {
                player.CurrentHealth = 0;
            }

            var input = $"{weapon.CombatDescription()}\n${enemy.CombatDescription()}";
            var severity = calculateSeverity(player.CurrentHealth, player.MaxHealth, weapon.Damage);
            var prompt = _promptService.GenerateDefenseDescription(input, CombatActionEnum.Melee, severity);
            var resultDescription = _aiService.Complete(prompt).Result;

            _printService.Type(resultDescription);
            _playerRepository.Update(player);
        }

        private (CombatActionEnum, string) determineIntendedAction()
        {
            while (true)
            {
                Console.WriteLine();
                var action = _printService.ReadInput();
                var prompt = _promptService.GenerateCombatIntentPrompt(action);
                var responseType = _aiService.Complete(prompt).Result?.Trim();

                var isValidEnum = CombatActionEnum.TryParse(responseType, out CombatActionEnum combatStatus);

                if (isValidEnum)
                    return (combatStatus, action);
                _printService.Print("The intended action was not understood");
            }
        }

        private bool meleeAttack(Player player, List<Npc> enemies, string text)
        {
            var weapon = player.CurrentWeapon;
            var target = enemies.Count == 1 ? enemies.First() : intendedTarget(enemies, text);
            var damage = calculateDamage(target?.Armor, weapon);

            target.CurrentHealth -= damage;

            if (target.CurrentHealth <= 0)
            {
                target.CurrentHealth = 0;
                target.IsAlive = false;
            }


            var input = $"{weapon.CombatDescription()}\n{target.CombatDescription()}\nAttempted Action: {text}";
            var severity = calculateSeverity(target.CurrentHealth, target.MaxHealth, weapon.Damage);
            var prompt = _promptService.GenerateAttackDescription(input, CombatActionEnum.Melee, severity);
            var resultDescription = _aiService.Complete(prompt).Result;

            _printService.Type(resultDescription);
            _npcRepository.Update(target);
            return true;
        }

        private bool spellAttack(Player player, List<Npc> enemies, string text)
        {
            return true;

        }

        private bool flee(Player player, List<Npc> enemies, string text)
        {
            var enemyNames = string.Join(", ", enemies.Select(x => x.FullName));
            _printService.ClearConsole();
            _printService.Type($"You have escaped from {enemyNames}");

            return false;

        }

        private bool divineHelp(Player player, List<Npc> enemies, string text)
        {
            var target = intendedTarget(enemies, text);
            _divineInterventionService.Smite(target);
            return true;
        }

        private bool talk(Player player, List<Npc> enemies, string text)
        {
            return true;
        }

        private bool pickupItem(Player player, List<Npc> enemies, string text)
        {
            return true;
        }


        private Npc intendedTarget(List<Npc> enemies, string text)
        {
            if (enemies.Count() == 1)
                return enemies.First();

            return null;
        }

        private bool validateAction(CombatActionEnum action, Player player)
        {
            return true;
        }

        private CombatSeverityEnum calculateSeverity(int currentHealth, int maxHealth, int damage)
        {
            if (currentHealth == 0)
                return CombatSeverityEnum.Kill;
            else if (damage / maxHealth > 4)
                return CombatSeverityEnum.Major;
            else if (damage == 0)
                return CombatSeverityEnum.Miss;
            else
                return CombatSeverityEnum.Minor;
        }   

        private int calculateDamage(Armor armor, Weapon weapon)
        {
            var damage = weapon.Damage - (armor?.PysicalResistance ?? 0);
            if (damage < 0)
                return 0;

            return damage;
        }
    }
}
