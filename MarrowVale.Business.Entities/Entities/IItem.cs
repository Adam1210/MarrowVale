
namespace MarrowVale.Business.Entities.Entities
{
    public interface IItem
    {
        string Name { get;  }        
        string Description { get; }
        string EnvironmentalDescription { get; }
        bool IsVisible { get;  }

        //Cost to buy/sell item
        int BaseWorth { get; }

        string GetDescription();
    }
}
