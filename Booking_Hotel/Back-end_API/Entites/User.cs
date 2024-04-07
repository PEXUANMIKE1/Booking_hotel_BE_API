using Back_end_API.Enumerates;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace Back_end_API.Entites
{
    [Table("Users_tbl")]
    public class User : Base
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string CCCD { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
