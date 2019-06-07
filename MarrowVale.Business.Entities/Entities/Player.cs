using MarrowVale.Business.Entities.Enums;
using System.Collections.Generic;

namespace MarrowVale.Business.Entities.Entities
{
    public class Player
    {
        public Player(RaceEnum race, string gender)
        {
            Race = race;
            Gender = gender;

            Abilities = new List<Ability>();
        }

        public string Name { get; set; }
        public ClassEnum Class { get; set; }
        public RaceEnum Race { get; private set; }
        public string Gender { get; private set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public Inventory Inventory { get; set; }
                
        public IList<Spell> Spellbook { get; private set; }

        public IList<Ability> Abilities { get; private set; }

        public Weapon CurrentWeapon { get; private set; }

        public void SwitchWeapon(Weapon newWeapon)
        {
            //needs checks added later
            Inventory.Items.Add(CurrentWeapon);
            CurrentWeapon = newWeapon;
        }
    }
}
