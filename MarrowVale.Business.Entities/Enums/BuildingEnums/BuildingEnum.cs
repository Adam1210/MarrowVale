using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarrowVale.Business.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BuildingEnum
    {
        Farm,
        MerchantHut,
        MerchantCart,
        MerchantShop,
        BlackSmith,
        SilverSmith,
        GoldSmith,
        ArcheryShop,
        MagicShop,
        PotionShop,
        Inn,
        Hut,
        House,
        Manor,
        Castle,
        Exchange
    }
}
