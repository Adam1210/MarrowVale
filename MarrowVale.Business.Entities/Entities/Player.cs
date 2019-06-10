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
        }

        public Player(PlayerDto player) : this()
        {
            Race = player.Race;
            Gender = player.Gender;
            Class = player.Class;
            Name = player.Name;
            
            if (Class == ClassEnum.Mage)
            {               
                MaxHealth = 15;
                CurrentHealth = 15;
            }
            else if(Class == ClassEnum.Ranger)
            {
                MaxHealth = 20;
                CurrentHealth = 20;
            }
            else
            {
                MaxHealth = 25;
                CurrentHealth = 25;
            }

            LastSaveDateTime = DateTime.Now;
        }

        [JsonConstructor]
        private Player(RaceEnum Race, string Gender, ClassEnum Class, string Name, DateTime LastSaveDateTime)
        {
            this.Race = Race;
            this.Gender = Gender;
            this.Class = Class;
            this.Name = Name;
            this.LastSaveDateTime = LastSaveDateTime;
        }

        public string Name { get;}
        public ClassEnum Class { get; }
        public RaceEnum Race { get; }
        public string Gender { get; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public Inventory Inventory { get; set; }
        public Location CurrentLocation { get; set; }
                
        public IList<Spell> Spellbook { get;}

        public IList<Ability> Abilities { get; }

        public Weapon CurrentWeapon { get; private set; }

        public DateTime LastSaveDateTime { get; set; } 

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
            return $"{Name}: {Race}  {Class}  - Last Saved {LastSaveDateTime.ToShortDateString()}";
        }
    }
}
