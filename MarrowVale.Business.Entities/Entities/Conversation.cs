using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarrowVale.Business.Entities.Entities
{
    public class Conversation : GraphNode
    {
        public Conversation()
        {
            this.EntityLabel = "Conversation";
            this.Labels = new List<string>() { EntityLabel, "Memory" };
        }
        public string Summary { get; set; }

    }
}
