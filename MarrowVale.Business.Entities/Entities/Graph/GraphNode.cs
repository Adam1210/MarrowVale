using MarrowVale.Business.Entities.Entities.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace MarrowVale.Business.Entities.Entities
{
    public class GraphNode
    {
        public GraphNode()
        {
            Labels = new List<string>();
            Relations = new List<BaseRelationship>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public IEnumerable<string> Labels { get; set; }
        [JsonIgnore]
        public IEnumerable<BaseRelationship> Relations { get; set; }
        [JsonIgnore]
        public IList<string> RelationshipLabel { get; set; }
        [JsonIgnore]
        public DateTimeOffset LastUpdated { get; set; }
        public string EntityLabel { get; set; }

        public virtual string FormattedLabels()
        {
            return string.Join(':', Labels);
        }

        public virtual string DescriptionPromptInput()
        {
            return "";
        }
    }
}
