using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("Hotel_tbl")]
    public class Hotel:Base
    {
        public string HotelName { get; set; }
        public string LocationID { get; set; }
        public Locations? Locations { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Hotline { get; set; }
        public Double PriceLow { get; set; }
        public IEnumerable<Room>? ListRooms { get; set; }
    }
}
