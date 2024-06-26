﻿
using Azure.Core;
using Back_end_API.Context;
using Back_end_API.Entites;
using Back_end_API.Handle;
using Back_end_API.Handle.Email;
using Back_end_API.Handle.Number;
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
    public class UserService : IUserService
    {
        private readonly ResponseObject<DataResponse_User> responseObject_User;
        private readonly ResponseObject<DataResponse_Token> responseObject_Token;
        private readonly UserConverter _userConverter;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly AppDbContext _context;
        public UserService(ResponseObject<DataResponse_User> responseObject_User, ResponseObject<DataResponse_Token> responseObject_Token, UserConverter userConverter, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            this.responseObject_User = responseObject_User;
            this.responseObject_Token = responseObject_Token;
            _userConverter = userConverter;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        //đăng ký
        public ResponseObject<DataResponse_User> Register(Request_Register Request)
        {
            if (string.IsNullOrWhiteSpace(Request.UserName)
                || string.IsNullOrWhiteSpace(Request.Password)
                || string.IsNullOrWhiteSpace(Request.FirstName)
                || string.IsNullOrWhiteSpace(Request.LastName)
                || string.IsNullOrWhiteSpace(Request.Address)
                || string.IsNullOrWhiteSpace(Request.CCCD)
                || string.IsNullOrWhiteSpace(Request.Phone)
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
            if (!Validate_Number.IsNumber(Request.CCCD))
            {
                return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Email không hợp lệ!", null);
            }
            if (!Validate_Number.IsNumber(Request.Phone))
            {
                return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Phone không hợp lệ!", null);
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
            user.CCCD = Request.CCCD;
            user.Phone = Request.Phone;
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
        //Đăng nhập
        public ResponseObject<DataResponse_Token> Login(Request_Login request)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName.Equals(request.UserName));
            if (user == null)
            {
                return responseObject_Token.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại!", null);
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
        // Đăng xuất
        public ResponseObject<DataResponse_Token> Logout()
        {
            var httpcontext = _httpContextAccessor.HttpContext;
            if(httpcontext != null && httpcontext.User.Identity.IsAuthenticated)
            {
                //lấy UserID đang đăng nhập
                var userIDLogin = httpcontext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIDLogin != null && int.TryParse(userIDLogin.Value, out int userID))
                {
                    var refreshToken = _context.RefeshTokens.SingleOrDefault(x => x.UserId == userID);
                    //Xóa refeshToken trên DB
                    if (refreshToken != null)
                    {
                        _context.RefeshTokens.Remove(refreshToken);
                        _context.SaveChanges();
                    }
                    //xóa AccessToken phía client
                    httpcontext.Session.Remove("AccessToken");
                    return responseObject_Token.ResponseSuccess("Đăng xuất thành công", null);
                }
                return responseObject_Token.ResponseError(StatusCodes.Status401Unauthorized,"Đăng xuất thành công", null);
            }
            return responseObject_Token.ResponseError(StatusCodes.Status401Unauthorized, "Tài khoản chưa xác thực", null);
        }
        //Làm mới token
        public DataResponse_Token RenewAccessToken(Request_RenewAccessToken request)
        {
            var refreshToken = _context.RefeshTokens.SingleOrDefault(x => x.Token.Equals(request.RefeshToken));
            var user = _context.Users.SingleOrDefault(x => x.ID == refreshToken.UserId);

            if (refreshToken.ExpiredTime > DateTime.Now)
            {
                throw new ArgumentException("Token da het han");
            }
            return GenerateAccessToken(user);
        }
        //Get All (chỉ Admin)
        public ResponseObject<IQueryable<DataResponse_User>> GetAll()
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<IQueryable<DataResponse_User>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tài khoản chưa xác thực",
                    Data = null
                };
            }
            if (currentUser.IsInRole("Admin"))
            {
                var user = _context.Users.Select(x => _userConverter.EntityDTO(x));
                if (!user.Any())
                {
                    return new ResponseObject<IQueryable<DataResponse_User>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống!",
                        Data = null
                    };
                }
                return new ResponseObject<IQueryable<DataResponse_User>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thực hiện thao tác thành công!",
                    Data = user
                };
            }
            return new ResponseObject<IQueryable<DataResponse_User>>
            {
                Status = StatusCodes.Status200OK,
                Message = "Bạn không đủ quyền hạn để sử dụng chức năng này!",
                Data = null
            };
        }
        //Sửa Người dùng (chỉ Admin)
        public ResponseObject<DataResponse_User> UpdateUser(int id, Request_Register Request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Người dùng chưa được xác thực!", null);
            }
            var user = _context.Users.SingleOrDefault(x => x.ID == id);
            if (user == null)
            {
                return responseObject_User.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
            }
            if (currentUser.IsInRole("Admin"))
            {
                if (string.IsNullOrWhiteSpace(Request.UserName)
                    || string.IsNullOrWhiteSpace(Request.Password)
                    || string.IsNullOrWhiteSpace(Request.FirstName)
                    || string.IsNullOrWhiteSpace(Request.LastName)
                    || string.IsNullOrWhiteSpace(Request.Address)
                    || string.IsNullOrWhiteSpace(Request.CCCD)
                    || string.IsNullOrWhiteSpace(Request.Phone)
                    || string.IsNullOrWhiteSpace(Request.Email))
                {
                    return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đẩy đủ thông tin!", null);
                }
                if (_context.Users.Any(x => x.Email.Equals(Request.Email, StringComparison.OrdinalIgnoreCase) && !Request.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại!", null);
                }
                if (_context.Users.Any(x => x.UserName.Equals(Request.UserName, StringComparison.OrdinalIgnoreCase) && !Request.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)))
                {
                    return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Tài khoản đã tồn tại!", null);
                }
                if (!Validate.IsValidEmail(Request.Email))
                {
                    return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Email không hợp lệ!", null);
                }
                if (!Validate_Number.IsNumber(Request.CCCD))
                {
                    return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Định dạng CCCD không hợp lệ!", null);
                }
                if (!Validate_Number.IsNumber(Request.Phone))
                {
                    return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Phone không hợp lệ!", null);
                }
                if (_context.Users.Any(x => x.CCCD.Equals(Request.CCCD) && !Request.CCCD.Equals(user.CCCD)))
                {
                    return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "CCCD này đã tồn tại!", null);
                }
                if (_context.Users.Any(x => x.Phone.Equals(Request.Phone) && !Request.Phone.Equals(user.Phone, StringComparison.OrdinalIgnoreCase)))
                {
                    return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Số điện thoại này đã tồn tại!", null);
                }
                user.UserName = Request.UserName;
                user.Email = Request.Email;
                user.FirstName = Request.FirstName;
                user.LastName = Request.LastName;
                user.Password = BCryptNet.HashPassword(Request.Password);
                user.DateOfBirth = Request.DateOfBirth;
                user.Gender = Request.Gender;
                user.Address = Request.Address;
                user.CCCD = Request.CCCD;
                user.Phone = Request.Phone;
                _context.Users.Update(user);
                _context.SaveChanges();
                return responseObject_User.ResponseSuccess("Sửa thông tin người dùng thành công!", _userConverter.EntityDTO(user));
            }
            return responseObject_User.ResponseError(StatusCodes.Status401Unauthorized, "Bạn không có quyền hạn để dùng chức năng này!", null);
        }
        //Sửa quyền hạn (chỉ Admin)
        public ResponseObject<DataResponse_User> UpdateRole(int UserId, int RoleIDUpdate)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return responseObject_User.ResponseError(StatusCodes.Status400BadRequest, "Người dùng chưa được xác thực!", null);
            }
            if (currentUser.IsInRole("Admin"))
            {
                var user = _context.Users.SingleOrDefault(x => x.ID == UserId);
                if (user == null)
                {
                    return responseObject_User.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
                }
                var role = _context.Roles.SingleOrDefault(role => role.ID == RoleIDUpdate);
                if (role == null)
                {
                    return responseObject_User.ResponseError(StatusCodes.Status404NotFound, "Quyền hạn này không tồn tại", null);
                }
                user.RoleId = role.ID;
                _context.Users.Update(user);
                _context.SaveChanges();
                return responseObject_User.ResponseSuccess("Sửa role người dùng thành công!", _userConverter.EntityDTO(user));
            }
            else
            {
                return responseObject_User.ResponseError(StatusCodes.Status401Unauthorized, "Bạn không có quyền hạn để dùng chức năng này!", null);
            }
        }
        //Xóa Người dùng (chỉ Admin)
        public ResponseObject<IQueryable<DataResponse_User>> Delete(int UserId)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<IQueryable<DataResponse_User>>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tài khoản chưa xác thực",
                    Data = null
                };
            }
            if (currentUser.IsInRole("Admin"))
            {
                var user = _context.Users.SingleOrDefault(u => u.ID == UserId);
                if(user is null)
                {
                    return new ResponseObject<IQueryable<DataResponse_User>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Người dùng này không tồn tại!",
                        Data = null
                    };
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                var ListUser = _context.Users.Select(x => _userConverter.EntityDTO(x));
                if (!ListUser.Any())
                {
                    return new ResponseObject<IQueryable<DataResponse_User>>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Danh sách trống!",
                        Data = null
                    };
                }
                return new ResponseObject<IQueryable<DataResponse_User>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thực hiện thao tác thành công!",
                    Data = ListUser
                };
            }
            return new ResponseObject<IQueryable<DataResponse_User>>
            {
                Status = StatusCodes.Status200OK,
                Message = "Bạn không đủ quyền hạn để sử dụng chức năng này!",
                Data = null
            };
        }
    }
}
