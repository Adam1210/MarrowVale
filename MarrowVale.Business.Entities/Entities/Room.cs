using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public class Room : Location
    {

        public Room()
        {
            this.EntityLabel = "Room";
            this.Labels = new List<string>() { "Location", EntityLabel };
        }
        public Room(Building building, RoomEnum roomType, int startingX, int startingY, int xLength, int yLength, int maxX, int maxY)
        {
            this.ParentBuilding = building;
            this.RoomType = roomType;
            SetCoordinates(startingX, startingY, xLength, yLength, maxX, maxY);
            this.Labels = new List<string>() { "Location", "Room" };
        }


        //Random Room Generator constructor  
        public Room(Building building)
        {
            var rng = new Random();
            var randomFloor = (FloorEnum)rng.Next(Enum.GetNames(typeof(FloorEnum)).Length);
            var randomWall = (WallEnum)rng.Next(Enum.GetNames(typeof(WallEnum)).Length);
            var randomRoom = (RoomEnum)rng.Next(Enum.GetNames(typeof(RoomEnum)).Length);

            building.Rooms.Add(this);
            this.ParentBuilding = building;
            this.RoomType = randomRoom;
            this.FloorType = randomFloor;
            this.WallType = randomWall;
            this.Id = Guid.NewGuid().ToString();

            //this.Items = generateItems();

            var connectedRooms = new List<Room>();

            if (rng.Next(0, 100) > 75)
            {
                connectedRooms.Add(new Room(building));
            }
            if (rng.Next(0, 100) > 75)
            {
                connectedRooms.Add(new Room(building));
            }
            if (rng.Next(0, 100) > 75)
            {
                connectedRooms.Add(new Room(building));

            }

            foreach (var connectedRoom in connectedRooms)
            {
                this.LeadsTo.Add(connectedRoom);
                connectedRoom.LeadsTo.Add(this);
            }
        }

        [JsonIgnore]
        public Building ParentBuilding { get; set; }
        public int Value { get; set; }
        public RoomEnum RoomType { get; set; }
        public WallEnum WallType { get; set; }
        public FloorEnum FloorType { get; set; }
        public IEnumerable<Item> Items { get; set; }

        public override StringBuilder DescriptionPromptInput()
        {
            var input = new StringBuilder();
            //var input = base.DescriptionPromptInput();
            //input.Append($"Building: {this.ParentBuilding.Name}");
            input.Append($"Room: {this.RoomType.ToString()}\n");
            input.Append($"Size: {CalculateSize()}\n");
            input.Append($"Interior: {this.WallType.ToString()}\n");
            input.Append($"Floor: {this.FloorType.ToString()}\n");
            return input;
        }

        public StringBuilder ShortDescriptionPrompt()
        {
            var input = new StringBuilder();
            input.Append($"Type: {this.RoomType.ToString()}\n");
            input.Append($"Size: {CalculateSize()}\n");
            input.Append($"Direction: {CalculateDirection()}\n");
            return input;
        }


        public override string CalculateSize()
        {
            //var area = SquareArea();

            var rng = new Random();
            var area = (rng.Next(0, 500));


            if (area > 1000)
                return "Enormous";
            else if (area > 500)
                return "Large";
            else if(area > 200)
                return "Normal Size";
            else if(area > 150)
                return "Small";
            else if(area > 50)
                return "Tiny";
            return "Miniscule";

        }

        public string CalculateDirection()
        {
            //var area = SquareArea();

            var rng = new Random();
            var area = (rng.Next(0, 1200));


            if (area > 1000)
                return "Right";
            else if (area > 500)
                return "Left";
            else if (area > 200)
                return "Front";
            else if (area > 150)
                return "Behind";
            else if (area > 50)
                return "Right";
            return "Left";

        }





    }
}
