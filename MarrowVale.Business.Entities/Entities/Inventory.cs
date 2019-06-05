using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Inventory
    {

        public Inventory()
        {
            Items = new List<IInventoryItem>();
        }

        public int Size { get; set; }
        public IList<IInventoryItem> Items { get; private set; }
    }
}
