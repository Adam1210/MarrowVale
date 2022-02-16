using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Player : GraphNode
    {
        public Player() {
            Abilities = new List<Ability>();
            Spellbook = new List<Spell>();
            Inventory = new Inventory();
            KnownLanguages = new List<LanguageEnum>();
            this.EntityLabel = "Player";
            this.Labels = new List<string>() { EntityLabel };
        }

        public Player(PlayerDto player) : this()
        {
            Id = Guid.NewGuid().ToString();
            Race = player.Race;
            Gender = player.Gender;
            Class = player.Class;
            Name = player.Name;
            KnownLanguages.Add(LanguageEnum.Common);

            switch (Race)
            {
                case PlayerRaceEnum.Dwarf:
                    KnownLanguages.Add(LanguageEnum.Dwarvish);
                    break;
                case PlayerRaceEnum.Elf:
                    KnownLanguages.Add(LanguageEnum.Elvish);
                    break;
                default:
                    break;
            }

            switch (Class) {
                case (ClassEnum.Mage):
                    MaxHealth = 15;
                    CurrentHealth = 15;
                    break;
                case (ClassEnum.Ranger):
                    MaxHealth = 20;
                    CurrentHealth = 20;
                    break;
                case (ClassEnum.Warrior):
                default:
                    MaxHealth = 25;
                    CurrentHealth = 25;
                    break;
            }

            LastSaveDateTime = DateTime.Now;
            GameSaveName = DateTime.Now.ToFileTime().ToString();
        }

        [JsonConstructor]
        private Player(Inventory Inventory, PlayerRaceEnum Race, string Gender, ClassEnum Class, string Name, DateTime LastSaveDateTime, IList<LanguageEnum> KnownLanguages,
                        int CurrentHealth)
        {
            this.Inventory = Inventory;
            this.CurrentHealth = CurrentHealth;
            this.Race = Race;
            this.Gender = Gender;
            this.Class = Class;
            this.Name = Name;
            this.LastSaveDateTime = LastSaveDateTime;
            this.KnownLanguages = KnownLanguages;
        }
        public ClassEnum Class { get; set; }
        public PlayerRaceEnum Race { get; set; }
        public string Gender { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        [JsonIgnore]
        public Inventory Inventory { get; set; }
        public string GameSaveName { get; set; }

        [JsonIgnore]
        private IList<Spell> Spellbook { get; }
        [JsonIgnore]
        private IList<Ability> Abilities { get; }

        [JsonIgnore]
        private IList<LanguageEnum> KnownLanguages { get; set; }
        [JsonIgnore]
        private IList<string> Buffs { get; set; }
        [JsonIgnore]
        public Weapon CurrentWeapon { get; set; }
        [JsonIgnore]
        public Armor CurrentArmor { get; set; }

        public DateTime LastSaveDateTime { get; set; } 

        public void Heal(int amount)
        {
            var tempHealth = CurrentHealth + amount;
            if(tempHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            else
            {
                CurrentHealth = tempHealth;
            }
        }

        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }


        public void Damage(int amount)
        {
            var tempHealth = CurrentHealth - amount;
            if(tempHealth > 0)
            {
                CurrentHealth = tempHealth;
            }
            else
            {
                //somehow notify user they died.
            }

        }

        public void SwitchWeapon(Weapon newWeapon)
        {
            //needs checks added later
            Inventory.AddItem<Weapon>(CurrentWeapon);
            CurrentWeapon = newWeapon;
        }

        public void AddSpellToSpellbook(Spell spell)
        {
            Spellbook.Add(spell);
        }

        public void AddAbility(Ability ability)
        {
            Abilities.Add(ability);
        }

        public string SaveInfo()
        {
            return $"{Name}: {Race}  {Class}  - Last Saved {LastSaveDateTime:MM/dd/yyyy  hh:mm}";
        }

        public int GetClimbingAbility()
        {
            //will be dependent on class/race
            return 1;
        }

        public void UpdateSaveFields()
        {
            LastSaveDateTime = DateTime.Now;
            GameSaveName = LastSaveDateTime.ToFileTime().ToString();
        }

        public string CombatDescription()
        {
            return $"Player: {Name} | Weapon: {CurrentWeapon?.Name} Armor: {CurrentArmor?.Name}";
        }
    }
}