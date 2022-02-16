using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public class Map : Area
    {
        public Map()
        {
            this.EntityLabel = "Map";
            this.Labels = new List<string>() { "Map" };
        }
        [JsonIgnore]
        public IList<Region> Regions { get; set; }

        public override string CalculateSize()
        {
            throw new NotImplementedException();
        }
    }
}
