using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities.Graph
{
    public class BaseRelationship
    {
        public string Relation { get; set; }
        public GraphNode Node { get; set; }
    }
}
