using Newtonsoft.Json;

namespace MarrowVale.Business.Entities.Entities
{ 
    public class GraphRelationship
    {
        public GraphRelationship()
        {

        }
        public GraphRelationship(string name) 
        {
            Name = name;
        }
        [JsonIgnore]
        public string Name { get; set; }


    }
}
