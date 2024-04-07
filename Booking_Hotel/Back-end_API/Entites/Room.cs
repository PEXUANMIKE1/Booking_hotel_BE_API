using Back_end_API.Enumerates;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("Room_tbl")]
    public class Room:Base
    {
        public int RoomTypeID { get; set; }
        public int HotelID { get; set; }
        public string RoomName { get; set; }
        public string Image {  get; set; }
        public Double Price { get; set; }
        public int MaxPerson { get; set; }
        public RoomStatus Status { get; set; }
        public RoomType? RoomType { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
