using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{

    [Table("ConfirmEmail_tbl")]
    public class ConfirmEmail:Base
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string CodeActive { get; set; }
        public bool IsConfirm { get; set; } = false;
    }
}
