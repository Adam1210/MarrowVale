using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarrowVale.Business.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WeaponTypeEnum
    {
        Sword,
        Bow,
        Axe,
        Staff,
        Wand
    }
}
