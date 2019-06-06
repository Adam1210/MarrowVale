
namespace MarrowVale.Business.Entities.Entities
{
    public class Player
    {
        public Player(RaceEnum race, string gender)
        {
            Race = race;
            Gender = gender;
        }

        public string Name { get; set; }
        public Class Class { get; set; }
        public RaceEnum Race { get; private set; }
        public string Gender { get; private set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public Inventory Inventory { get; set; }
                

        public Weapon CurrentWeapon { get; private set; }

        public void SwitchWeapon(Weapon newWeapon)
        {
            //needs checks added later
            Inventory.Items.Add(CurrentWeapon);
            CurrentWeapon = newWeapon;
        }
    }
}
