using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class EnvironmentalObject
    {
        [JsonConstructor]
        public EnvironmentalObject(string Name, string Description, string MovedDescription = null, bool Moved = false)
        {
            this.Name = Name;
            this.Description = Description;
            this.MovedDescription = MovedDescription;
            this.Moved = Moved;
        }

        public string Name { get; }
        public string Description { get; }
        public bool Moved { get; private set; }
        public string MovedDescription { get; }

        public string GetDescription()
        {
            if (Moved)
            {
                return $"{Description}{Environment.NewLine}{MovedDescription}";
            }
            else
            {
                return Description;
            }
        }
    }
}
