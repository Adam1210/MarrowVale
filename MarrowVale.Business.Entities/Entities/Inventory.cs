using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MarrowVale.Business.Entities.Entities
{
    public class Inventory
    {

        public Inventory()
        {
            Items = new List<IItem>();
            Size = 15;
            CurrentCurrency = 0;
            MaxCurrency = 50;
        }

        public int Size { get; set; }
        public IList<IItem> Items { get; private set; }

        public int CurrentCurrency { get; set; }
        public int MaxCurrency { get; private set; }

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
