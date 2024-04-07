using System.Text.Json.Serialization;

namespace Back_end_API.Enumerates
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusPay
    {
        //Chưa thanh toán
        Unpaid = 0,
        //Đã thanh thanh toán
        Paid = 1,
        //Đã cọc tiền
        Deposited = 2
    }
}
