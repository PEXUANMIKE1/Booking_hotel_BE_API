using Back_end_API.Enumerates;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("Booking_tbl")]
    public class Booking:Base
    {
        public int RoomID { get; set; }
        public Room Room { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public StatusBooking status { get; set; }
        public PaymentMethod PayMethod { get; set; }
        public int NumberOfPeople { get; set; }
        public string Note { get; set; }
        public Double Total { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
