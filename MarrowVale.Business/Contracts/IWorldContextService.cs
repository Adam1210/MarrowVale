using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Contracts
{
    public interface IWorldContextService
    {
        public GraphNode GetObjectLabelIdPair(string searchObject, Player owner, string input);
        public T DirectObjectCommand<T>(string input, string context, GraphNode owner, CommandEnum command) where T : GraphNode;
        public void AttachToContext(string label, string id, Object entity);
        public string GenerateRoadFlavorText(Location road);
        public string GenerateBuildingFlavorText(Location building);
        public string GenerateRoomFlavorText(Location building);
        public string DescribeConnectedPaths(Player player);
        public GraphNode ContextSearch(string searchTerm, string conversation);
        public GraphNode ContextSearch(string searchTerm, GraphNode characterKnowledge);
    }
}
