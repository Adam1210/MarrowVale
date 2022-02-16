using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarrowVale.Business.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnviornmentEnum
    {
        Forest,
        Mountain,
        Swamp,
        Desert,
        Ocean,
        Tundra
    }
}
