using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public class Building : Location
    {
        public Building()
        {
            Rooms = new List<Room>();
            this.EntityLabel = "Building";
            this.Labels = new List<string>() { "Location", EntityLabel };
        }

        public int Value { get; set; }
        public BuildingEnum BuildingType { get; set; }
        public BuildingEnum LandScapeType { get; set; }
        public BuildingEnum Exterior { get; set; }
        [JsonIgnore]
        public IList<Room> Rooms { get; set; }
        [JsonIgnore]
        public Room Entrance { get; set; }
        [JsonIgnore]
        public bool[,] GridLayout { get; set; }
        public override StringBuilder DescriptionPromptInput()
        {
            //var input = base.DescriptionPromptInput();
            var input = new StringBuilder();
            input.Append($"Building: {this.Name}\n");
            input.Append($"Type: {this.BuildingType.ToString()}\n");
            input.Append($"Size: {CalculateSize()}\n");
            input.Append($"Landscape: {LandScapeType.ToString()}\n");
            input.Append($"Size: {Exterior.ToString()}\n");
            return input;
        }

        public StringBuilder ShortExteriorDescription()
        {
            var input = base.DescriptionPromptInput();
            input.Append($",Type: {this.BuildingType.ToString()}, Size: {this.CalculateSize()}\n");
            return input;
        }

        public override string CalculateSize()
        {
            //var area = SquareArea();
            var rng = new Random();
            var area = (rng.Next(0, 1000));

            if (area > 10000)
                return "Enormous";
            else if (area > 5000)
                return "Large";
            else if(area > 1000)
                return "Normal Size";
            else if(area > 500)
                return "Small";
            else if(area > 250)
                return "Tiny";
            return "Miniscule";

        }

        public void GenerateRooms()
        {
            this.Entrance = new Room(this);
        }


    }
}
