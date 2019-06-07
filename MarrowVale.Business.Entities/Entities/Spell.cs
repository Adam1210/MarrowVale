
using MarrowVale.Business.Entities.Enums;

namespace MarrowVale.Business.Entities.Entities
{
    public class Spell
    {
        public Spell()
        {

        }

        public string Name { get; protected set; }
        public string Description { get; set; }
        public int Damage { get; protected set; }
        public SpellElementEnum Element { get; protected set; }

        public int NumberOfUses { get; protected set; }
        public int AvailableNumberOfUses { get; private set; }

        public int UseSpell()
        {
            if (AvailableNumberOfUses == 0)
            {
                //display some message about having no uses or something
                return 0;
            }

            AvailableNumberOfUses--;

            return Damage;
        }
    }
}
