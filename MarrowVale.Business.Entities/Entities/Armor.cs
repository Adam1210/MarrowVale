using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Armor : Item
    {
        public Armor() {
            this.EntityLabel = "Armor";
            this.Labels = new List<string>() { "Item", EntityLabel };
        }

        [JsonConstructor]
        public Armor(string Name, string Description, int BaseWorth, string Type, int PhysicalResistance, int MagicResistance) : this()
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
        public string Type { get; set; }
        public int PysicalResistance { get; set; }
        public int MagicResistance { get; set; }

        public void Show()
        {
            IsVisible = true;
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
