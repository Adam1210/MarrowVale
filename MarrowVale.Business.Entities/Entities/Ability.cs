using Newtonsoft.Json;

namespace MarrowVale.Business.Entities.Entities
{
    public class Ability
    {
        [JsonConstructor]
        public Ability(string Name, string Description, GameCommand Command)
        {
            this.Name = Name;
            this.Description = Description;
            this.Command = Command;
        }

        public string Name { get; }

        public string Description { get; }
        public GameCommand Command { get; }
    }
}
