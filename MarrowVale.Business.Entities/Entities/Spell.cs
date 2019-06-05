
namespace MarrowVale.Business.Entities.Entities
{
    public class Spell
    {
        public string Name { get; protected set; }

        // Not sure how spells going to work. Is it Mana based or some other resource?
        public int Cost { get; protected set; }
    }
}
