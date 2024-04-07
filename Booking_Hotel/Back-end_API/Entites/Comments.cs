using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("Comments_tbl")]
    public class Comments : Base
    {
        public int UserID { get; set; }
        public User? User { get; set; }
        public int HotelID { get; set; }
        public Hotel? Hotel { get; set; }
        public int Comment { get; set; }
        public DateTime CommentDay { get; set; }
    }
}
