
namespace MarrowVale.Business.Entities.Entities
{
    public class Ability
    {
        public Ability(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }

        public string Description { get; }
    }
}
