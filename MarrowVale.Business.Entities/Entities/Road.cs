using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public class Road : Location
    {
        public Road()
        {
            this.EntityLabel = "Road";
            this.Labels = new List<string>() { "Location", EntityLabel };
        }
        public bool StartOfRoad { get; set; }
        public int Length { get; set; }
        public string Units { get; set; }
        [JsonIgnore]
        public Coordinate Start { get; set; }

        public StringBuilder ShortExteriorDescription()
        {
            //var input = base.DescriptionPromptInput();
            var input = new StringBuilder();
            input.Append($"Road: {this.Name}");
            input.Append($",Length: {this.LengthDescription()}");
            return input;
        }

        public string LengthDescription()
        {
            var randomLength = new Random().Next(0, 1000);
            //Change Based on Location
            if (randomLength > 1000)
                return "Very Long";
            else if (randomLength > 500)
                return "Long";
            else if (randomLength > 200)
                return "Short";
            else if (randomLength > 150)
                return "Very Short";
            else if (randomLength > 50)
                return "Tiny";
            return "The size you know it was probably built for gnomes";

        }

    }
}
