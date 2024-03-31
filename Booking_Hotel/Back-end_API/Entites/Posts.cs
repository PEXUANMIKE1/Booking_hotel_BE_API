using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("Posts_tbl")]
    public class Posts: Base
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int Title { get; set; }
        public int Content { get; set; }
        public DateTime PostDay { get; set; }
    }
}
