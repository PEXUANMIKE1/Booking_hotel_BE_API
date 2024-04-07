using System.Text.Json.Serialization;

namespace Back_end_API.Enumerates
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusBooking
    {
        //đã hủy
        Canceled = 0,
        //đã nhận
        Received = 1,
        //Đang chờ
        Pending = 2
    }
}
