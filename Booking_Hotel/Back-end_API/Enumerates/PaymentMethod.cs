using System.Text.Json.Serialization;

namespace Back_end_API.Enumerates
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentMethod
    {
        Cash = 0,
        Online = 1,
    }
}
