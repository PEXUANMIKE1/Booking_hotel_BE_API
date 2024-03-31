using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("RefeshToken_tbl")]
    public class RefeshToken : Base
    {
        public string Token { get; set; }
        public DateTime ExpiredTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
