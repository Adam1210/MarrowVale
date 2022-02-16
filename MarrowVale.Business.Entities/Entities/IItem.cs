
using MarrowVale.Common.Utilities;

namespace MarrowVale.Business.Entities.Entities
{
    public class Item : GraphNode
    {
        public Item()
        {

        }
        public string EnvironmentalDescription { get; set; }
        public bool IsVisible { get; set; }
        public int BaseWorth { get; set; }
        public virtual string GetShortDescription()
        {
            return IsVisible ? $"{Name} - {Description} - {CurrencyUtility.StandardizeCurrency(BaseWorth)}" : "";
        }
        public virtual string GetDescription()
        {
            return "";
        }

        private string convertCurrency(int currency)
        {
            return $"{currency} bronze";  
        }
    }
}
