using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities.Graph
{
    public class GraphNodeData
    {
        public GraphNode Data { get; set; }
        public IEnumerable<BaseRelationship> Relations { get; set; }
    }
}
