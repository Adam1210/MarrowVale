using MarrowVale.Business.Entities.Entities;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Data.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(IGraphClient graphClient) : base(graphClient)
        {
        }
        public Room GetBuildingEntrance(string buildingId)
        {
            return RelatedTo<Room, GraphRelationship>(x => x.Id == buildingId, y => true, new GraphRelationship("PATH")).ResultsAsync.Result.FirstOrDefault();
        }

        public Road GetBuildingExit(string buildingId)
        {
            return RelatedTo<Road, GraphRelationship>(x => x.Id == buildingId, y => true, new GraphRelationship("PATH")).ResultsAsync.Result.FirstOrDefault();
        }

        public List<Building> GetNearbyBuildings(Location location)
        {
            return RelatedTo<Building, GraphRelationship>(x => x.Id == location.Id, y => true, new GraphRelationship("PATH")).ResultsAsync.Result.ToList();
        }

        public List<Road> GetNearbyRoads(Location location)
        {
            return RelatedTo<Road, GraphRelationship>(x => x.Id == location.Id, y => true, new GraphRelationship("PATH")).ResultsAsync.Result.ToList();
        }

        public List<Npc> GetNpcsAtLocation(Location location)
        {
            return RelatedTo<Npc,GraphRelationship>(x => x.Id == location.Id,y=> true,new GraphRelationship("INSIDE"), false).ResultsAsync.Result.ToList();
        }

        public List<Room> GetConnectingRooms(Location location)
        {
            return RelatedTo<Room, GraphRelationship>(x => x.Id == location.Id, y => true, new GraphRelationship("PATH")).ResultsAsync.Result.ToList();
        }

        public bool IsPathConnected(Location location, Location location2)
        {
            return RelatedTo<Location, GraphRelationship>(x => x.Id == location.Id, y => y.Id == location2.Id, new GraphRelationship("PATH*..2")).ResultsAsync.Result.Any();
        }

    }
}
