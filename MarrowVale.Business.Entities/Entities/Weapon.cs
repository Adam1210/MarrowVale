using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Weapon : Item
    {
        public Weapon() {
            this.EntityLabel = "Weapon";
            this.Labels = new List<string>() { "Item", EntityLabel };
        }

        [JsonConstructor]
        public Weapon(string Name, string Description, int BaseWorth, WeaponTypeEnum Type, int Damage, int Range) : this()
        {
            this.BaseWorth = BaseWorth;
            this.Damage = Damage;
            this.Range = Range;
            this.Type = Type;
            this.Name = Name;
            this.Description = Description;
        }


        public int Range { get; set; }
        public int Damage { get; set; }
        public int DamageRange { get; set; }
        public bool IsBroken { get; set; }
        public WeaponTypeEnum Type { get; set; }

        public void Show()
        {
            IsVisible = true;
        }

        public string CombatDescription()
        {
            return $"Weapon: {Name} | Damage: {Damage}";
        }

        public override string GetShortDescription()
        {
            if (IsVisible)
                return Type.ToString();
            return "";
        }

        public override string GetDescription()
        {
            return $"{Description}{Environment.NewLine}{EnvironmentalDescription}";
        }

        public override string ToString()
        {
            return $"{Name} - {GetDescription()}";
        }
    }
}
