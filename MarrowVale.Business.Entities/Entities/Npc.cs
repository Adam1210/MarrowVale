using MarrowVale.Business.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    // This would most likely be a base class, and may want to make more enemy types that inherit this.
    public class Npc
    {
        public Npc()
        {
            Abilities = new List<Ability>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }

        public IList<Ability> Abilities { get; }

        public NpcTypeEnum Type { get; private set; }
    }
}
