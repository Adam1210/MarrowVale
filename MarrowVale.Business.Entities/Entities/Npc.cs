using MarrowVale.Business.Entities.Enums;
using System.Collections.Generic;


namespace MarrowVale.Business.Entities.Entities
{
    // This would most likely be a base class, and may want to make more enemy types that inherit this.
    public class Npc
    {
        public Npc()
        {
            Abilities = new List<Ability>();
            SpellBook = new List<Spell>();
            Items = new List<IItem>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int BaseDamage { get; }

        public IList<Ability> Abilities { get; }
        public IList<Spell> SpellBook { get; }
        public IList<IItem> Items { get; }

        public NpcTypeEnum Type { get; private set; }
    }
}
