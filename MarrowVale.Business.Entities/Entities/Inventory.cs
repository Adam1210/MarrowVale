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

        public void AddItem(IItem item)
        {
            if(Items.Count >= Size)
            {
                //add some sort of notification or indication it failed
                return;
            }

            Items.Add(item);
        }
    }
}
