using MarrowVale.Business.Entities.Entities;

namespace MarrowVale.Data.Seeder.Items
{
    public static class WeaponsSeed
    {
        public static Weapon GetWoodenSword()
        {
            var woodenSword = new Weapon("Wooden Sword", "Fragile piece of wood that comes to a point",1, Business.Entities.Enums.WeaponTypeEnum.Sword,1,5);
            
            return woodenSword;
        }
    }
}
