using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Commands;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Data.Contracts;
using System;
using System.Linq;


namespace MarrowVale.Business.Services
{
    public class CommandProcessingService : ICommandProcessingService
    {
        private readonly IWorldContextService _worldContextService;
        private readonly IDialogueService _dialogueService;
        private readonly IPlayerRepository _playerRepository;
        private readonly INpcRepository _npcRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ICombatService _combatService;
        private readonly IDivineInterventionService _divineInterventionService;
        private readonly IPrintService _printService;

        public CommandProcessingService(IWorldContextService worldContextService, IPlayerRepository playerRepository, IDialogueService dialogueService,
                                        INpcRepository npcRepository, ILocationRepository locationRepository, ICombatService combatService, IDivineInterventionService divineInterventionService,
                                        IPrintService printService)
        {
            _worldContextService = worldContextService;
            _playerRepository = playerRepository;
            _dialogueService = dialogueService;
            _npcRepository = npcRepository;
            _locationRepository = locationRepository;
            _combatService = combatService;
            _divineInterventionService = divineInterventionService;
            _printService = printService;
        }

        public string ShowWorld(Player player)
        {
            var currentLocationType = _playerRepository.PlayerLocationType(player);
            var currentLocation = _playerRepository.GetPlayerLocation(player);
            if (currentLocationType.Contains("Road"))
            {
                return _worldContextService.GenerateRoadFlavorText(currentLocation);
            }
            else if (currentLocationType.Contains("Building"))
            {
                return _worldContextService.GenerateBuildingFlavorText(currentLocation);
            }
            else if (currentLocationType.Contains("Room"))
            {
                return _worldContextService.GenerateRoomFlavorText(currentLocation);
            }
            return "Error";
        }



        public string ProcessCommand(Command command, Player player) =>
            command?.Type switch
            {
                CommandEnum.Enter => Enter(command, player),
                CommandEnum.Traverse => TraverseRoad(command, player),
                CommandEnum.Speak => speak(command, player),
                CommandEnum.Inventory => inspectInventory(player),
                CommandEnum.Health => inspectHealth(player),
                CommandEnum.Attack => attack(command, player),
                CommandEnum.Exit => ExitBuilding(command, player),
                _ => "Command Is Not Mapped to a function",
            };


        private string speak(Command command, Player player)
        {
            var npc = _npcRepository.Single(x => x.Id == command.DirectObjectNode.Id).Result;
            if (isAbleToSpeakWith(player, npc))
            {
                _dialogueService.Talk(player, npc);
            }
            else
            {
                var error = _divineInterventionService.HandleError(command.Input, $"Unable to speak with character {npc?.FullName ?? command?.DirectObjectNode?.Name}");
                _printService.PrintDivineText(error);
                return "";
            }

            return "Conversation Over.";
        }

        private string Enter(Command command, Player player)
        {
            var goToLocation = _locationRepository.GetById(command.DirectObjectNode.Id).Result;
            if (isValidPath(player, goToLocation))
            {
                //_locationRepository.GetLocation(command.DirectObjectId);
                Location enteredLocation;

                if (command.DirectObjectNode.Labels.Contains("Building"))
                    enteredLocation = _locationRepository.GetBuildingEntrance(command.DirectObjectNode.Id);
                else
                    enteredLocation = _locationRepository.Single(x => x.Id == command.DirectObjectNode.Id).Result;
                var currentLocation = _playerRepository.GetPlayerLocation(player);
                _playerRepository.MovePlayer(player, currentLocation.Id, enteredLocation.Id);
                return _worldContextService.GenerateRoomFlavorText(enteredLocation);
            }
            else
            {
                var error = _divineInterventionService.HandleError(command.Input, $"Unable to navigate the following path to {goToLocation?.Name ?? command?.DirectObjectNode?.Name}");
                _printService.PrintDivineText(error);
                return "";
            }

            
        }

        private string ExitBuilding(Command command, Player player)
        {
            var exit = _locationRepository.GetBuildingExit(command.DirectObjectNode.Id);
            if (isValidPath(player, exit))
            {
                var currentLocation = _playerRepository.GetPlayerLocation(player);
                _playerRepository.MovePlayer(player, currentLocation.Id, exit.Id);
                return ShowWorld(player);
            }
            else
            {
                var error = _divineInterventionService.HandleError(command.Input, $"Unable to exit building {exit?.Name ?? command?.DirectObjectNode?.Name}");
                _printService.PrintDivineText(error);
                return "";
            }
        }

        private string TraverseRoad(Command command, Player player)
        {
            //if (isValidPath(player, null))
            //{
            //    var currentLocation = _playerRepository.GetPlayerLocation(player);
            //    _playerRepository.MovePlayer(player, currentLocation.Item2, command.DirectObjectName);

            //    var building = new Building { Name = command.DirectObjectName };
            //    return _worldContextService.GenerateFlavorText(building);
            //}

            return "Invalid Path";
        }

        private bool isValidPath(Player player, Location location)
        {
            var currentLocation = _playerRepository.GetPlayerLocation(player);
            return _locationRepository.IsPathConnected(currentLocation, location);
        }

        private bool isAbleToSpeakWith(Player player, Npc npc)
        {
            return _npcRepository.IsPlayerNearby(player, npc);
        }
        private bool isAbleToAttack(Player player, Npc npc)
        {
            return _npcRepository.IsPlayerNearby(player, npc);
        }

        private string inspectInventory(Player player)
        {
            var inventory = _playerRepository.GetInventory(player);
            foreach (var item in inventory.Items)
            {
                Console.WriteLine(item.GetShortDescription());
            }

            return $"{player.Name}'s capicity is";
        }

        private string inspectHealth(Player p)
        {
            var player = _playerRepository.GetById(p.Id).Result;
            return $"You are at {player.CurrentHealth}/{player.MaxHealth} health"; 
        }

        private string attack(Command command, Player player)
        {
            var npc = _npcRepository.GetById(command.DirectObjectNode.Id).Result;
            _npcRepository.SetCombatEquipment(npc);

            if (isAbleToAttack(player, npc))
            {
                return _combatService.Attack(player, npc);
            }
            else
            {
                var error =_divineInterventionService.HandleError(command.Input, $"Unable to attack character {player?.Name ?? command?.DirectObjectNode?.Name}");
                _printService.PrintDivineText(error);
                return "";
            }
        }

    }
}
