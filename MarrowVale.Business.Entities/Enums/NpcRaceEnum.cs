using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarrowVale.Business.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NpcRaceEnum
    {
        Human,
        Elf,
        Dwarf,
        Dragonkin,
        Goblin,
        Troll,
        Orc
    }
}
