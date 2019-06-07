using MarrowVale.Business.Entities.Enums;

namespace MarrowVale.Business.Entities.Entities
{
    public class Weapon : IItem
    {
        public Weapon(int worth)
        {
            BaseWorth = worth;
        }

        public int Range { get; protected set; }
        public int Damage { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsBroken { get; set; }
        public WeaponTypeEnum Type { get; private set; }
        public bool IsVisible { get; set; }
        public int BaseWorth { get; }
    }
}
