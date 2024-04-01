using Back_end_API.Entites;
using Back_end_API.Enumerates;

namespace Back_end_API.Payload.DataResponses
{
    public class DataResponse_User
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }
    }
}
