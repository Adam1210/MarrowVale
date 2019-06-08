using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System.ComponentModel;

namespace MarrowVale.Business.Entities.Entities
{
    public class Weapon : IItem
    {
        public Weapon(int worth)
        {
            BaseWorth = worth;
        }

        public int Range { get; set; }
        public int Damage { get; set; }
        public int DamageRange { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsBroken { get; set; }
        public WeaponTypeEnum Type { get; private set; }
        public bool IsVisible { get; set; }
        public int BaseWorth { get; set; }
    }
}
