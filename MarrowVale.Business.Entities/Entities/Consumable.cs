using Newtonsoft.Json;

namespace MarrowVale.Business.Entities.Entities
{
    public class Consumable : Item
    {
        [JsonConstructor]
        public Consumable(int BaseWorth, string Name, string Description, string EnvironmentalDescription, bool IsVisible)
        {
            this.BaseWorth = BaseWorth;
            this.Name = Name;
            this.IsVisible = IsVisible;
            this.Description = Description;
            this.EnvironmentalDescription = EnvironmentalDescription;
        }


        public void Show()
        {
            IsVisible = true;
        }

        public override string GetShortDescription()
        {
            if (IsVisible)
                return Name.ToString();
            return "";
        }
        public override string GetDescription()
        {
            // Add the logic to build this description.
            return Description;
        }
        public override string ToString()
        {
            return $"{Name} - {GetDescription()}";
        }
    }
}
