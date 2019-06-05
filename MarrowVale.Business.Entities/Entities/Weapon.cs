
namespace MarrowVale.Business.Entities.Entities
{
    public class Weapon : IInventoryItem
    {
        public int Range { get; protected set; }
        public int Damage { get; protected set; }
        public string Name { get; set; }
    }
}
