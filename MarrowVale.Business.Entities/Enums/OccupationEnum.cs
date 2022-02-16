using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarrowVale.Business.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OccupationEnum
    {
        Merchant,
        Mercenary,
        Farmer,
        Unemployed,
        Apprentice,
        Blacksmith,
        Miner,
        Cobbler,
        InnKeeper,
        TownMaster,
        Duke,
        King
    }
}
