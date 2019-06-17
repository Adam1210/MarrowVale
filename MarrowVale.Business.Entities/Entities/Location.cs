
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Location
    {
        public Location()
        {
            Items = new List<IItem>();
            Npcs = new List<Npc>();
            EnvironmentalObjects = new List<EnvironmentalObject>();
        }

        public Location(string Name, string Description, IList<EnvironmentalObject> environmentalObjects): this()
        {
            this.Name = Name;
            this.Description = Description;
            this.EnvironmentalObjects = environmentalObjects;
        }
        
        public string Name { get; }
        public string Description { get; private set; }

        public IList<IItem> Items { get; private set; }

        public IList<Npc> Npcs { get; private set; }
        private IList<EnvironmentalObject> EnvironmentalObjects { get; set; }

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
