using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Class
    {
        public Class()
        {
            StartingWeapons = new List<Weapon>();
            StartingSpells = new List<Spell>();
            StartingAmmunition = new List<Ammunition>();
            StartingConsumables = new List<Consumable>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Weapon> StartingWeapons { get; set; }
        public IList<Spell> StartingSpells { get; set; }
        public IList<Ammunition> StartingAmmunition { get; set; }
        public IList<Consumable> StartingConsumables { get; set; }
    }
}
