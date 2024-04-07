using Back_end_API.Enumerates;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("Bookings_tbl")]
    public class Booking : Base
    {
        public int RoomID { get; set; }
        public Room? Room { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
        public int ExtraServiceID { get; set; }
        public ExtraServices? ExtraServices { get; set; }
        //Trạng thái đặt phòng
        public StatusBooking StatusBooking { get; set; }
        //trạng thái thanh toán
        public StatusPay StatusPay { get; set; }
        //Phương thức thanh toán
        public PaymentMethod PayMethod { get; set; }
        //Tiền cọc
        public Double? Deposit {  get; set; }
        //Tổng tiền
        public Double? Total { get; set; }
        public int NumberOfPeople { get; set; }
        public string? Note { get; set; }
        public DateTime BookingDate { get; set; } 
        public DateTime? CheckInDate { get; set; } = null;
        public DateTime CheckOutDate { get; set; }
    }
}
