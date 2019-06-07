
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Location
    {
        public Location()
        {
            Items = new List<IItem>();
            Npcs = new List<Npc>();
        }
        
        public IList<IItem> Items { get; private set; }

        public IList<Npc> Npcs { get; private set; }

        public void AddItem(IItem item)
        {
            Items.Add(item);
        }
    }
}
