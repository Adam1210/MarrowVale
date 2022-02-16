using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace MarrowVale.Business.Entities.Enums.Combat
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CombatSeverityEnum
    {
        Minor,
        Major,
        Kill,
        Miss
    }
}
