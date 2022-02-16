using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public class Deity : GraphNode
    {
        public Deity()
        {
            this.EntityLabel = "Character";
            this.Labels = new List<string>() { EntityLabel };
        }

        public int Blessing { get; set; }
    }
}
