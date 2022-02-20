using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public class PromptSubType : GraphNode
    {
        public PromptSubType()
        {
            this.EntityLabel = "SubPromptType";
            this.Labels = new List<string>() { EntityLabel };
        }
    }
}
