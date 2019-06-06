using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Inventory
    {

        public Inventory()
        {
            Items = new List<IItem>();
        }

        public int Size { get; set; }
        public IList<IItem> Items { get; private set; }
    }
}
