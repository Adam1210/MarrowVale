using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;

namespace MarrowVale.Business.Entities.Entities
{
    public class Weapon : IItem
    {
        [JsonConstructor]
        public Weapon(string Name, string Description, int BaseWorth, WeaponTypeEnum Type, int Damage, int Range)
        {
            this.BaseWorth = BaseWorth;
            this.Damage = Damage;
            this.Range = Range;
            this.Type = Type;
            this.Name = Name;
            this.Description = Description;
        }

        public int Range { get; private set; }
        public int Damage { get; private set; }
        public int DamageRange { get; private set; }
        public string Name { get; }
        public string Description { get; }
        public bool IsBroken { get; private set; }
        public WeaponTypeEnum Type { get; }
        public bool IsVisible { get; private set; }
        public int BaseWorth { get; }

        public string EnvironmentalDescription { get; private set; }

        public string GetDescription()
        {
            // Add the logic to build this description.
            return Description;
        }
    }
}
