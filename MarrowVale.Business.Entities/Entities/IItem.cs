
namespace MarrowVale.Business.Entities.Entities
{
    public interface IItem
    {
        string Name { get; set; }        
        string Description { get; set; }
        bool IsVisible { get; set; }

        //Cost to buy/sell item
        int BaseWorth { get; }
    }
}
