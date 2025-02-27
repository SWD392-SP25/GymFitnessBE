using FirebaseAdmin.Auth;
using GymFitness.API.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GymFitness.Domain.Services;
using GymFitness.Domain.Abstractions.Services;
using GymFitness.Infrastructure.Data;
using GymFitness.API.Services.Abstractions;
using GymFitness.Domain.Entities;
using GymFitness.Application.Services;
using GymFitness.Application.Abstractions.Services;
using Google.Apis.Auth.OAuth2.Requests;
using System.Security.Cryptography;

namespace GymFitness.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFirebaseAuthService _firebaseAuthService;
        private readonly IUserService _userService;
        private readonly StaffService _staffService;
        private readonly IRedisService _redisService;

        public AuthController(IConfiguration configuration, IFirebaseAuthService firebaseAuthService,
                                                            IUserService userService, StaffService staffService,
                                                            IRedisService redisService)
        {
            _configuration = configuration;
            _firebaseAuthService = firebaseAuthService;
            _userService = userService;
            _staffService = staffService;
            _redisService = redisService;
        }

        private string GenerateJwtToken(string id, string email, string role)
        {
            var jwtKey = _configuration["JwtSettings:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException("Jwt:Key", "JWT key is not configured.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, id),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] GoogleLoginRequestDto request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.IdToken))
                {
                    return BadRequest(new { message = "ID Token is required" });
                }
                // ✅ Xác thực ID token từ Firebase
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.IdToken);
                string email = decodedToken.Claims["email"]?.ToString();
                string uid = decodedToken.Uid;

                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized(new { message = "Invalid Firebase token: missing email" });
                }

                Console.WriteLine($"Email extracted from token: {email}");

                // ✅ Kiểm tra xem user hoặc staff đã tồn tại trong DB chưa

                Console.WriteLine($"Checking user with email: {email}");
                var user = await _userService.GetUserByEmail(email);
                Console.WriteLine(user != null ? $"User found Auth Layer: {user.UserId}" : "User not found");

                //Console.WriteLine($"Checking staff with email: {email}");
                //var staff = await _staffService.GetStaffByEmail(email);
                //Console.WriteLine(staff != null ? $"Staff found: {staff.StaffId}" : "Staff not found");

                Console.WriteLine("Check if user is already exist");
                if (user == null)
                {
                    // Nếu email chưa tồn tại trong cả 2 bảng -> Tạo User mới
                    user = new User
                    {
                        UserId = Guid.NewGuid(),
                        Email = email,
                        RoleId = 1
                    };

                     _userService.AddUser(user);
                }

                // ✅ Xác định đối tượng đăng nhập & lấy thông tin
                string role;
                string id;

                Console.WriteLine("Take user from db and transfer data to jwt");
                if (user != null)
                {
                    Console.WriteLine("Adding user to jwt token");
                    id = user.UserId.ToString();
                    role = user.Role.Name;
                }
                //else if(staff != null)
                //{
                //    id = staff.StaffId.ToString();
                //    role = "Staff";
                //}
                else
                {
                    throw new Exception("User not found");

                }


                // ✅ Tạo JWT token
                Console.WriteLine("calling GenerateJwtToken ");
                string jwtToken = GenerateJwtToken(id, email, role);
                var refreshToken = GenerateRefreshToken();

                // ✅ Lưu token vào Redis
                Console.WriteLine("Save token to redis");
                await _redisService.SetAsync($"refresh:{id}", refreshToken, TimeSpan.FromDays(7));


                Console.WriteLine("return jwtToken");
                return Ok(new { Token = jwtToken, RefreshToken = refreshToken, Id = id, Email = email, Role = role });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken)) return BadRequest("Refresh token is required.");

            // ✅ Kiểm tra Access Token có bị thu hồi không
            var isRevoked = await _redisService.GetAsync($"revoke:{request.AccessToken}");
            if (!string.IsNullOrEmpty(isRevoked))
            {
                return Unauthorized("Access Token has been revoked.");
            }

            // ✅ Lấy refresh token từ Redis
            var storedRefreshToken = await _redisService.GetAsync($"refresh:{request.UserId}");
            if (storedRefreshToken != request.RefreshToken) return Unauthorized("Invalid refresh token.");

            // ✅ Tạo Access Token mới
            var user = await _userService.GetUserById(request.UserId);
            if (user == null) return Unauthorized("User not found.");

            string jwtToken = GenerateJwtToken(user.UserId.ToString(), user.Email, user.Role.Name);
            return Ok(new { Token = jwtToken });
        }


        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto request)
        {
            // Xoá Refresh Token trong Redis để không thể refresh nữa
            await _redisService.DeleteAsync($"refresh:{request.UserId}");

            // Đánh dấu Access Token đã bị thu hồi
            await _redisService.SetAsync($"revoke:{request.AccessToken}", "true", TimeSpan.FromHours(3));

            return Ok(new { message = "Logged out successfully" });
        }
    }
}
