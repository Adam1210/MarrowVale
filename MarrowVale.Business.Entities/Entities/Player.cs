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
                case RaceEnum.Dwarf:
                    KnownLanguages.Add(LanguageEnum.Dwarvish);
                    break;
                case RaceEnum.Elf:
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
        }

        [JsonConstructor]
        private Player(Inventory Inventory, RaceEnum Race, string Gender, ClassEnum Class, string Name, DateTime LastSaveDateTime, IList<LanguageEnum> KnownLanguages,
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
        public RaceEnum Race { get; }
        public string Gender { get; }
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public Inventory Inventory { get; }
        public Location CurrentLocation { get; set; }
                
        private IList<Spell> Spellbook { get;}

        private IList<Ability> Abilities { get; }

        [JsonProperty]
        private IList<LanguageEnum> KnownLanguages { get; set; }

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
    }
}
