﻿using MarrowVale.Business.Entities.Dtos;
using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel;

namespace MarrowVale.Business.Entities.Entities
{
    public class Player
    {
        public Player() { }
        public Player(PlayerDto player)
        {
            Race = player.Race;
            Gender = player.Gender;
            Class = player.Class;
            Name = player.Name;

            Abilities = new List<Ability>();
            Spellbook = new List<Spell>();
            Inventory = new Inventory();

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
        }

        public string Name { get; set; }
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

        public void SwitchWeapon(Weapon newWeapon)
        {
            //needs checks added later
            Inventory.Items.Add(CurrentWeapon);
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
    }
}
