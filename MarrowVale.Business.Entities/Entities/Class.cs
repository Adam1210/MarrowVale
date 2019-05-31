using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class Class
    {
        public string Name { get; set; }
        public List<Weapon> StartingWeapons { get; set; }
        public List<Spell> StartingSpells { get; set; }
        public List<Ammunition> StartingAmmunition { get; set; }
        public List<Consumable> StartingConsumables { get; set; }
    }
}
