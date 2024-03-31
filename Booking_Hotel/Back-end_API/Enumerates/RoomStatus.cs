using System.Text.Json.Serialization;

namespace Back_end_API.Enumerates
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoomStatus
    {
        Empty = 0,
        Booked = 1,
        Occupied = 2,
        Dirty = 3,
        Unavailable = 4,
    }
}
