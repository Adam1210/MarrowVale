
namespace MarrowVale.Business.Entities.Entities
{
    public class Ability
    {
        public Ability(string name, string description, GameCommand command)
        {
            Name = name;
            Description = description;
            Command = command;
        }

        public string Name { get; }

        public string Description { get; }
        public GameCommand Command { get; }
    }
}
