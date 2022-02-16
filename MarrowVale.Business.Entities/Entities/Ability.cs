using Newtonsoft.Json;
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Ability : GraphNode
    {
        [JsonConstructor]
        public Ability(string Name, string Description, GameCommand Command)
        {
            this.Name = Name;
            this.Description = Description;
            this.Command = Command;
            this.EntityLabel = "Ability";
            this.Labels = new List<string>(){ EntityLabel };
        }

        public GameCommand Command { get; }
    }
}
