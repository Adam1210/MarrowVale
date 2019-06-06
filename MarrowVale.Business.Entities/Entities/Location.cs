
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Location
    {
        public Location()
        {
            Items = new List<IItem>();
        }
        
        public IList<IItem> Items { get; set; }

        public void AddItem(IItem item)
        {
            Items.Add(item);
        }
    }
}
