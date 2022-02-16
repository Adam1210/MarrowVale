using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarrowVale.Business.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SentimentEnum
    {
        Happy,
        Crazy,
        Sarcastic,
        Suprised,
        Tired,
        Angry,
        Anxious,
        Excited
    }
}
