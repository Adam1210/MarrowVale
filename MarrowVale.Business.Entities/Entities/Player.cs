using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Player
    {
        public Player() {
            Abilities = new List<Ability>();
            Spellbook = new List<Spell>();
            Inventory = new Inventory();
            KnownLanguages = new List<LanguageEnum>();
        }

        public Player(PlayerDto player) : this()
        {
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

        public string Name { get;}
        public ClassEnum Class { get; }
        public PlayerRaceEnum Race { get; }
        public string Gender { get; }
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public Inventory Inventory { get; }

        public string GameSaveName { get; set; }
                
        private IList<Spell> Spellbook { get; }
        private IList<Ability> Abilities { get; }

        [JsonProperty]
        private IList<LanguageEnum> KnownLanguages { get; set; }
        [JsonProperty]
        private IList<string> Buffs { get; set; }

        public Weapon CurrentWeapon { get; private set; }

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
            Inventory.AddItem(CurrentWeapon);
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
    }
}
