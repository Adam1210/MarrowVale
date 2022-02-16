using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public class Region : Area
    {
        public Region()
        {
            this.EntityLabel = "Region";
            this.Labels = new List<string>() { "Location", EntityLabel };
        }
        public EnviornmentEnum Environment { get; set; }
        [JsonIgnore]
        public IList<City> Cities { get; set; }
        [JsonIgnore]
        public IList<Location> Locations { get; set; }

        public override string CalculateSize()
        {
            throw new NotImplementedException();
        }
    }
}
