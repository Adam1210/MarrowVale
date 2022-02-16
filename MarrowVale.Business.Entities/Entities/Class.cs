using MarrowVale.Business.Entities.Enums;
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{

    public class Class : GraphNode
    {
        public Class()
        {
            StartingWeapons = new List<Weapon>();
            StartingSpells = new List<Spell>();
            StartingAmmunition = new List<Ammunition>();
            StartingConsumables = new List<Consumable>();
            StartingAbilities = new List<Ability>();
            this.EntityLabel = "Class";
            this.Labels = new List<string>() { EntityLabel };
        }

        public ClassEnum Type { get; set; }
        public IList<Weapon> StartingWeapons { get; set; }
        public IList<Spell> StartingSpells { get; set; }
        public IList<Ammunition> StartingAmmunition { get; set; }
        public IList<Consumable> StartingConsumables { get; set; }
        public IList<Ability> StartingAbilities {get;set;}
    }
}
