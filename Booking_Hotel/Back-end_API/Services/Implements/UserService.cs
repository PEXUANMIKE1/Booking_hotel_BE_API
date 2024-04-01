using Azure.Core;
using Back_end_API.Entites;
using Back_end_API.Handle;
using Back_end_API.Handle.Email;
using Back_end_API.Payload.Converters;
using Back_end_API.Payload.DataRequests;
using Back_end_API.Payload.DataResponses;
using Back_end_API.Payload.Response;
using Back_end_API.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Back_end_API.Services.Implements
{
    public class UserService : BaseService, IUserService
    {
        private readonly ResponseObject<DataResponse_User> responseObject_User;
        private readonly ResponseObject<DataResponse_Token> responseObject_Token;
        private readonly UserConverter _userConverter;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            responseObject_User = new ResponseObject<DataResponse_User>();
            responseObject_Token = new ResponseObject<DataResponse_Token>();
            _userConverter = new UserConverter();
            _configuration = configuration;
        }

        //đăng ký
        public ResponseObject<DataResponse_User> Register(Request_Register Request)
        {
            if (string.IsNullOrWhiteSpace(Request.UserName)
                || string.IsNullOrWhiteSpace(Request.Password)
                || string.IsNullOrWhiteSpace(Request.FirstName)
                || string.IsNullOrWhiteSpace(Request.LastName)
                || string.IsNullOrWhiteSpace(Request.Address)
                || string.IsNullOrWhiteSpace(Request.Email))
            {
                return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đẩy đủ thông tin!", null);
            }
            if (_context.Users.Any(x => x.Email.Equals(Request.Email)))
            {
                return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại!", null);
            }
            if (_context.Users.Any(x => x.UserName.Equals(Request.UserName)))
            {
                return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Tài khoản đã tồn tại!", null);
            }
            if (!Validate.IsValidEmail(Request.Email))
            {
                return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Email không hợp lệ!", null);
            }
            var user = new User();
            user.UserName = Request.UserName;
            user.Email = Request.Email;
            user.FirstName = Request.FirstName;
            user.LastName = Request.LastName;
            user.Password = BCryptNet.HashPassword(Request.Password);
            user.DateOfBirth = Request.DateOfBirth;
            user.Gender = Request.Gender;
            user.Address = Request.Address;
            user.RoleId = 3;
            _context.Users.Add(user);
            _context.SaveChanges();
            return responseObject_User.ResponseSuccess("Đăng ký tài khoản thành công!", _userConverter.EntityDTO(user));
        }
        private string GenerateRefeshToken()
        {
            var random = new byte[32];
            using (var item = RandomNumberGenerator.Create())
            {
                item.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        //Tạo token và refesh Token
        public DataResponse_Token GenerateAccessToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);
            var role = _context.Roles.SingleOrDefault(x => x.ID == user.RoleId);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("ID",user.ID.ToString()),
                    new Claim("Email",user.Email),
                    new Claim(ClaimTypes.Role,role.Code),
                }),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refeshToken = GenerateRefeshToken();

            RefeshToken rf = new RefeshToken()
            {
                Token = refeshToken,
                ExpiredTime = DateTime.Now.AddDays(1),
                UserId = user.ID,
            };
            _context.RefeshTokens.Add(rf);
            _context.SaveChanges();

            DataResponse_Token result = new DataResponse_Token
            {
                AccessToken = accessToken,
                RefeshToken = refeshToken
            };
            return result;
        }

        //đăng nhập
        public ResponseObject<DataResponse_Token> Login(Request_Login request)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName.Equals(request.UserName));
            if (user == null)
            {
                return responseObject_Token.ResponseError(StatusCodes.Status400BadRequest, "Có lỗi trong quá trình xử lý!", null);
            }
            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                return responseObject_Token.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đẩy đủ thông tin!", null);
            }
            bool checkPass = BCryptNet.Verify(request.Password, user.Password);
            if (!checkPass)
            {
                return responseObject_Token.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu không chính xác!", null);
            }
            return responseObject_Token.ResponseSuccess("Đăng nhập thành công", GenerateAccessToken(user));
        }

        //Làm mới token
        public DataResponse_Token RenewAccessToken(Request_RenewAccessToken request)
        {
            throw new NotImplementedException();
        }
        //Get All
        public IQueryable<DataResponse_User> GetAll()
        {
            var user = _context.Users.Select(x => _userConverter.EntityDTO(x));
            return user;
        }
    }
}
