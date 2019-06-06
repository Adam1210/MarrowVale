
namespace MarrowVale.Business.Entities.Entities
{
    public class Location
    {
        public Location()
        { }

        public Location(IItem item)
        {
            Item = item;
        }

        public IItem Item { get; set; }
    }
}
