//using MarrowVale.Business.Entities.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MarrowVale.Business.Entities.Entities.Buildings
//{
//    public class Manor : Building
//    {
//        public override void GenerateRooms()
//        {
//            var buildingOriginX = BorderBottomLeft.X;
//            var buildingOriginy = BorderBottomLeft.Y;
//            var buildingMaxX = BorderBottomRight.X;
//            var buildingMaxY = BorderTopRight.Y;
//            GridLayout = new bool[xLength(), yLength()];
//            var rng = new Random();
//            var percentOfHouse = rng.Next(5, 15);
//            var roomLength = (SquareArea() * percentOfHouse) / 100;
//            var roomCenterOffset = roomLength / 2;
//            var houseCenter = xLength() / 2;
//            var roomOriginX = buildingOriginX + houseCenter - roomCenterOffset;
//            var roomOriginY = buildingOriginy;


//            var initialRoom = new Room(this, RoomEnum.Foyer, roomOriginX, roomOriginY, roomLength, roomLength, buildingMaxX, buildingMaxY);
//            SetGridAreaOccupied(roomOriginX, roomOriginY, roomLength, roomLength);
//        }

//        private Room generateRoom(Room room, string direction)
//        {



//            if (direction == "Left")
//            {
//                var buildingOriginX = BorderBottomLeft.X;
//                var buildingOriginy = BorderBottomLeft.Y;
//                var buildingMaxX = BorderBottomRight.X;
//                var buildingMaxY = BorderTopRight.Y;
//                var rng = new Random();
//                var percentOfHouse = rng.Next(5, 15);
//                var roomLength = (SquareArea() * percentOfHouse) / 100;




//                var roomOriginX = buildingOriginX;
//                var roomOriginY = buildingOriginy;
//            }



//            var randomFloor = rng.Next(Enum.GetNames(typeof(WallEnum)).Length);
//            var randomWall = rng.Next(Enum.GetNames(typeof(FloorEnum)).Length);
//            var randomRoom = rng.Next(Enum.GetNames(typeof(RoomEnum)).Length);

//            var newRoom = new Room(this, (RoomEnum)randomRoom, roomOriginX, roomOriginY, roomLength, roomLength, buildingMaxX, buildingMaxY);
//            var connectedRooms = new List<Room>();

//            if (canAddRoomLeft(newRoom))
//            {
//                connectedRooms.Add(generateRoom(newRoom, "Left"));
//            }
//            if (canAddRoomRight(newRoom))
//            {
//                connectedRooms.Add(generateRoom(newRoom, "Right"));
//            }
//            if (canAddRoomUp(newRoom))
//            {
//                connectedRooms.Add(generateRoom(newRoom, "Up"));

//            }
//            if (canAddRoomDown(newRoom))
//            {
//                connectedRooms.Add(generateRoom(newRoom, "Down"));
//            }

//            foreach (var connectedRoom in connectedRooms)
//            {
//                newRoom.LeadsTo.Add(connectedRoom);
//                connectedRoom.LeadsTo.Add(newRoom);
//            };
//        }


//        public bool canAddRoomLeft(Room room)
//        {
//            -1 when looking to left
//            return IsAnySpaceAvailable(room.BorderBottomLeft.X - 1, room.BorderBottomLeft.Y, 1, yLength());
//        }

//        public bool canAddRoomRight(Room room)
//        {
//            return IsAnySpaceAvailable(room.BorderBottomRight.X, room.BorderBottomRight.Y, 1, yLength());
//        }

//        public bool canAddRoomUp(Room room)
//        {
//            return IsAnySpaceAvailable(room.BorderTopLeft.X, room.BorderBottomLeft.Y, xLength(), 1);
//        }

//        public bool canAddRoomDown(Room room)
//        {
//            -1 when looking down
//            return IsAnySpaceAvailable(room.BorderBottomLeft.X, room.BorderBottomLeft.Y - 1, xLength(), 1);
//        }


//        public void SetGridAreaOccupied(int originX, int originY, int xLength, int yLength)
//        {
//            var xOffset = BorderBottomLeft.X;
//            var yOffset = BorderBottomLeft.Y;
//            originX -= xOffset;
//            originY -= yOffset;


//            for (int x = originX; x <= (originX + xLength); x++)
//            {
//                for (int y = originX; y <= (originX + xLength); y++)
//                {
//                    GridLayout[x, y] = true;
//                }
//            }
//        }

//        public bool IsGridOccupied(int originX, int originY, int xLength, int yLength)
//        {
//            var xOffset = BorderBottomLeft.X;
//            var yOffset = BorderBottomLeft.Y;
//            originX -= xOffset;
//            originY -= yOffset;

//            for (int x = originX; x <= (originX + xLength); x++)
//            {
//                for (int y = originX; y <= (originX + xLength); y++)
//                {
//                    if (GridLayout[x, y] == true)
//                        return false;
//                }
//            }
//            return true;
//        }


//        public bool IsAnySpaceAvailable(int originX, int originY, int xLength, int yLength)
//        {
//            var xOffset = BorderBottomLeft.X;
//            var yOffset = BorderBottomLeft.Y;
//            originX -= xOffset;
//            originY -= yOffset;



//            for (int x = originX; x <= (originX + xLength); x++)
//            {
//                for (int y = originX; y <= (originX + xLength); y++)
//                {
//                    if (x < 0 || x < GridLayout.GetLength(0) || y < 0 || y < GridLayout.GetLength(1))
//                        return false;
//                    if (GridLayout[x, y] == false)
//                        return true;
//                }
//            }
//            return false;
//        }


//    }
//}
