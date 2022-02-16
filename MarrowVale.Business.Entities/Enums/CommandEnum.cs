using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarrowVale.Business.Entities.Enums
{

    //**If Enum name changes, reflect changes in InputProccesing Service and Prompt Json: Console/MarrowVale/PromptFile/InterpretCommand**
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommandEnum
    {
        Inventory,
        MoveToward,
        Traverse,
        Enter,
        Exit,
        ClimbUp,
        ClimbDown,
        Swim,
        Dance,
        Take,
        Give,
        Attack,
        Equip,
        Use,
        Cast,
        Health,
        Abilities,
        Speak,
        Purchase,
        Sell
    }
}
