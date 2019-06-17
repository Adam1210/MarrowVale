using Newtonsoft.Json;

namespace MarrowVale.Business.Entities.Entities
{
    public class Consumable : IItem
    {
        [JsonConstructor]
        public Consumable(int BaseWorth, string Name, string Description, string EnvironmentalDescription)
        {
            this.BaseWorth = BaseWorth;
            this.Name = Name;
            this.Description = Description;
            this.EnvironmentalDescription = EnvironmentalDescription;
        }

        public string Name { get; }
        public string Description { get; }
        public string EnvironmentalDescription { get; private set; }
        public bool IsVisible { get; private set; }
        public int BaseWorth { get; }

        public string GetDescription()
        {
            // Add the logic to build this description.
            return Description;
        }
    }
}
