using Newtonsoft.Json;

namespace MarrowVale.Business.Entities.Entities
{
    public class Ammunition : IItem
    {
        [JsonConstructor]
        public Ammunition(string Name, string Description, int Count, bool IsVisible, int BaseWorth, string EnvironmentalDescription)
        {
            this.Name = Name;
            this.Description = Description;
            this.Count = Count;
            this.IsVisible = IsVisible;
            this.BaseWorth = BaseWorth;
            this.EnvironmentalDescription = EnvironmentalDescription;
        }

        public string Name { get; }
        public string Description { get; }
        public int Count { get; private set; }
        public bool IsVisible { get; private set; }
        public int BaseWorth { get; }

        public string EnvironmentalDescription { get; private set; }
        
        public void SetEnvironmentalDescription(string description)
        {
            EnvironmentalDescription = description;
        }

        public void Show()
        {
            IsVisible = true;
        }

        public void Use()
        {
            if(Count > 0)
            {
                Count--;
            }
        }

        public string GetDescription()
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
