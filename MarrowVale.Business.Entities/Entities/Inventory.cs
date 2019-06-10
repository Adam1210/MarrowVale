using Newtonsoft.Json;
using System.Collections.Generic;

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

        [JsonConstructor]
        private Inventory(int CurrentCurrency, int MaxCurrency, int Size)
        {
            this.CurrentCurrency = CurrentCurrency;
            this.MaxCurrency = MaxCurrency;
            this.Size = Size;
        }

        public int Size { get; private set; }
        private IList<IItem> Items { get; set; }

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
