using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;

namespace MarrowVale.Business.Entities.Entities
{
    public class Spell
    {
        [JsonConstructor]
        public Spell(string Name, string Description, int Damage, SpellElementEnum Element, int NumberOfUses, int AvailableNumberOfUses)
        {
            this.Name = Name;
            this.Description = Description;
            this.Damage = Damage;
            this.Element = Element;
            this.NumberOfUses = NumberOfUses;
            this.AvailableNumberOfUses = AvailableNumberOfUses;
        }

        public string Name { get; }
        public string Description { get; }
        public int Damage { get; private set; }
        public SpellElementEnum Element { get; }

        public int NumberOfUses { get; private set; }
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
