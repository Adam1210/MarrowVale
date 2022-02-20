using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public class PromptType : GraphNode
    {
        public PromptType()
        {
            this.EntityLabel = "PromptType";
            this.Labels = new List<string>() { EntityLabel };
        }

        public PromptType(string type)
        {
            this.Name = type;
        }

    }
}
