using System.Text.Json.Serialization;

namespace Back_end_API.Enumerates
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusBooking
    {
        Canceled = 0,
        Received = 1,
        Confirmed = 2,
        Paid = 3,
        Checked_out = 4,
        Pending = 5
    }
}
