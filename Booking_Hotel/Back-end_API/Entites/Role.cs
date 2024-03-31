using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("Role_tbl")]
    public class Role : Base
    {
        public string Code { get; set; }
        public string RoleName { get; set; }
    }
}
