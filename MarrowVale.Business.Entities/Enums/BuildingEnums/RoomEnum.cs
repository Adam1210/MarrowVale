using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarrowVale.Business.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoomEnum
    {
        Foyer,
        Bedroom,
        BallRoom,
        CourtYard,
        Hallway,
        DiningRoom,
        Kitchen,
        Cellar,
        Lobby,
        Bathroom,
        Bar,
        MainRoom
    }
}
