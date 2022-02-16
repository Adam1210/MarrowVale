using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarrowVale.Business.Entities.Entities
{
    public class Inventory : GraphNode
    {
        public Inventory()
        {
            Id = Guid.NewGuid().ToString();
            Items = new List<Item>();
            Size = 15;
            CurrentCurrency = 0;
            MaxCurrency = 50;
            this.EntityLabel = "Inventory";
            this.Labels = new List<string>() { EntityLabel };
        }

        [JsonConstructor]
        private Inventory(int CurrentCurrency, int MaxCurrency, int Size, IList<Item> Items)
        {
            this.CurrentCurrency = CurrentCurrency;
            this.MaxCurrency = MaxCurrency;
            this.Size = Size;
            this.Items = Items;
        }

        public int Size { get; private set; }
        [JsonIgnore]
        public IEnumerable<Item> Items { get; set; }
        public int CurrentCurrency { get; private set; }
        public int MaxCurrency { get; private set; }

        public void AddCurrency(int amountToAdd)
        {
            var tempCurrency = CurrentCurrency + amountToAdd;
            if(tempCurrency > MaxCurrency)
            {
                CurrentCurrency = MaxCurrency;
                //ToDo: notify user they are at max and lost currency/left currency
            }
            else
            {
                CurrentCurrency = tempCurrency;
            }
        }

        public void AddItem<T>(T item) where T : Item
        {
            if (Items.Count() >= Size)
            {
                //add some sort of notification or indication it failed
                return;
            }

            Items = Items.Append(item);
        }

        public Item DropItem(string name)
        {
            //var itemToDrop = Items.FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            //if(itemToDrop != null)
            //{
            //    Items.(itemToDrop);
            //}

            //return itemToDrop;
            return null;
        }

        public string GetItems()
        {
            return string.Join($"{Environment.NewLine}", Items.Select(x => x.ToString()));
        }
    }
}
