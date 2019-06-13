using MarrowVale.Business.Entities.Entities;

namespace MarrowVale.Data.Seeder.Items
{
    public static class WeaponsSeed
    {
        public static Weapon GetWoodenSword()
        {
            var woodenSword = new Weapon(1, Business.Entities.Enums.WeaponTypeEnum.Sword);

            woodenSword.Damage = 1;
            woodenSword.Name = "Wooden Sword";
            woodenSword.Description = "Fragile piece of wood that comes to a point";

            return woodenSword;
        }
    }
}
