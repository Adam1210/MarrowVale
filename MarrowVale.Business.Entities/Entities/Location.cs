
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class Location
    {
        public Location()
        {
            Items = new List<IItem>();
            Npcs = new List<Npc>();
        }

        [JsonConstructor]
        public Location(string Name, string Description, IList<EnvironmentalObject> EnvironmentalObjects, IList<EnvironmentalInteraction> EnvironmentalInteractions): this()
        {
            this.Name = Name;
            this.Description = Description;
            this.EnvironmentalObjects = EnvironmentalObjects;
            this.EnvironmentalInteractions = EnvironmentalInteractions;
        }
        
        public string Name { get; }
        public string Description { get; private set; }

        [JsonProperty]
        private IList<IItem> Items { get; }

        [JsonProperty]
        private IList<Npc> Npcs { get; }
        private IList<EnvironmentalObject> EnvironmentalObjects { get; set; }
        private IList<EnvironmentalInteraction> EnvironmentalInteractions { get; set; }
        
        private IList<string> ConnectingLocationNames { get; set; }

        public void AddItem(IItem item)
        {
            Items.Add(item);
        }

        public IItem PickUpItem(string item)
        {
            return Items.FirstOrDefault(x => x.Name.Equals(item, StringComparison.CurrentCultureIgnoreCase));
        }

        public string GetLocationDescription()
        {
            //first attempt at building location description
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Description);
            stringBuilder.AppendJoin($"{Environment.NewLine}", Items.Where(x=>x.IsVisible).ToString());
            stringBuilder.AppendJoin($"{Environment.NewLine}", EnvironmentalObjects.ToString());
            return stringBuilder.ToString();
        }
    }
}
