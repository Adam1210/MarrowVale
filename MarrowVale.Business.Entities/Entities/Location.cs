
using Newtonsoft.Json;
using System.Collections.Generic;

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

        public IList<IItem> Items { get; private set; }

        public IList<Npc> Npcs { get; private set; }
        private IList<EnvironmentalObject> EnvironmentalObjects { get; set; }
        private IList<EnvironmentalInteraction> EnvironmentalInteractions { get; set; }

        public void AddItem(IItem item)
        {
            Items.Add(item);
        }

        public string GetLocationDescription()
        {
            //prints out description based on Items and time of day
            return "";
        }
    }
}
