using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarrowVale.Data.Contracts
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        Room GetBuildingEntrance(string buildingId);
        Road GetBuildingExit(string buildingId);
        List<Building> GetNearbyBuildings(Location location);
        List<Road> GetNearbyRoads(Location location);
        List<Npc> GetNpcsAtLocation(Location location);
        List<Room> GetConnectingRooms(Location location);
        bool IsPathConnected(Location location, Location location2);
    }
}
