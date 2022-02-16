using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarrowVale.Business.Entities.Enums
{

    //**If Enum name changes, reflect changes in InputProccesing Service and Prompt Json: Console/MarrowVale/PromptFile/InterpretCommand**
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CombatActionEnum
    {
        Melee,
        Spell,
        Flee,
        PickUpItem,
        Talk,
        Ability,
        DivineHelp,
        Unknown
    }
}
