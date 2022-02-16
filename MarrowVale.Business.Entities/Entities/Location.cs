
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class Location : Area
    {
        public Location()
        {
            LeadsTo = new List<Location>();
            Items = new List<Item>();
            Npcs = new List<Npc>();
            this.EntityLabel = "Location";
            this.Labels = new List<string>() { EntityLabel };
        }

        [JsonConstructor]
        public Location(string Name, string Description, IList<EnvironmentalObject> EnvironmentalObjects, IList<EnvironmentalInteraction> EnvironmentalInteractions): this()
        {
            this.Name = Name;
            this.Description = Description;
            this.EnvironmentalObjects = EnvironmentalObjects;
            this.EnvironmentalInteractions = EnvironmentalInteractions;
        }

        [JsonIgnore]
        private IList<Item> Items { get; }
        [JsonIgnore]
        private IList<Npc> Npcs { get; }
        [JsonIgnore]
        private IList<EnvironmentalObject> EnvironmentalObjects { get; set; }
        [JsonIgnore]
        private IList<EnvironmentalInteraction> EnvironmentalInteractions { get; set; }
        [JsonIgnore]
        public IList<Location> LeadsTo { get; set; }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public Item PickUpItem(string item)
        {
            return Items.FirstOrDefault(x => x.Name.Equals(item, StringComparison.CurrentCultureIgnoreCase));
        }

        public virtual StringBuilder DescriptionPromptInput()
        {
            var description = new StringBuilder();
            description.Append($"Name: {this.Name}");
            return description;
        }


        public override string CalculateSize()
        {
            var area = SquareArea();
            //Change Based on Location
            if (area > 1000)
                return "Enormous";
            else if (area > 500)
                return "Large";
            else if (area > 200)
                return "Normal Size";
            else if (area > 150)
                return "Samll";
            else if (area > 50)
                return "Tiny";
            return "Miniscule";

        }
    }
}
