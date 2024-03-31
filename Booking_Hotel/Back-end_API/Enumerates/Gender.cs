using System.Text.Json.Serialization;

namespace Back_end_API.Enumerates
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        MALE = 1,
        FEMALE = 2,
        UNDEFINE = 0
    }
}
