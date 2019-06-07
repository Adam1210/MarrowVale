
namespace MarrowVale.Business.Entities.Entities
{
    public class Consumable : IItem
    {
        public Consumable(int worth)
        {
            BaseWorth = worth;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public int BaseWorth { get; }
    }
}
