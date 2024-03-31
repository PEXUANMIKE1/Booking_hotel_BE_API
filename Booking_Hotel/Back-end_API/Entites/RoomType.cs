using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("RoomType_tbl")]
    public class RoomType:Base
    {
        public string RoomTypeName { get; set; }
        public string Description { get; set; }
    }
}
