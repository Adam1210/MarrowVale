using Newtonsoft.Json;
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Ammunition : Item
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
            this.EntityLabel = "Ammunition";
            this.Labels = new List<string>() { EntityLabel };
        }

        public int Count { get; private set; }
        
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
        public override string GetShortDescription()
        {
            // Add the logic to build this description.
            return Description;
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
