using Back_end_API.Context;
using Back_end_API.Entites;
using Back_end_API.Payload.DataResponses;

namespace Back_end_API.Payload.Converters
{
    public class UserConverter
    {
        private readonly AppDbContext _context;
        public UserConverter()
        {
            _context = new AppDbContext();
        }
        public DataResponse_User EntityDTO(User user)
        {
            return new DataResponse_User
            {
                UserName = user.UserName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Address = user.Address,
                RoleName = _context.Roles.SingleOrDefault(x => x.ID == user.RoleId).RoleName
            };
        }
    }
}
