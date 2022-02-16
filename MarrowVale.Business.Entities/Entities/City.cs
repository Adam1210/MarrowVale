using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public class City : Area
    {
        public City()
        {
            this.EntityLabel = "City";
            this.Labels = new List<string>() { "Location", EntityLabel };
        }
        public int Population { get; set; }
        public List<Location> Locations { get; set;}

        public override string CalculateSize()
        {
            throw new NotImplementedException();
        }
    }
}
