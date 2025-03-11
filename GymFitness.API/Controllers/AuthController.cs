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
using Microsoft.AspNetCore.Authorization;

namespace GymFitness.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFirebaseAuthService _firebaseAuthService;
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        private readonly IRedisService _redisService;

        public AuthController(IConfiguration configuration, IFirebaseAuthService firebaseAuthService,
                                                            IUserService userService, IStaffService staffService,
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
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
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

                // ✅ Kiểm tra xem User đã tồn tại trong DB chưa
                var user = await _userService.GetUserByEmail(email);
                var staff = user == null ? await _staffService.GetByEmailAsync(email) : null;

                if (user == null && staff == null)
                {
                    // ✅ Nếu email chưa tồn tại trong cả 2 bảng -> Tạo User mới
                    Console.WriteLine("User & Staff not found -> Creating new User");

                    user = new User
                    {
                        UserId = Guid.NewGuid(),
                        Email = email,
                        Status = "Active",
                        RoleId = 1 // Mặc định là User
                    };

                     await _userService.AddUser(user);
                }

                // ✅ Xác định thông tin đăng nhập
                string id, role;
                if (user != null)
                {
                    id = user.UserId.ToString();
                    role = user?.Role?.Name ?? "User";
                }
                else
                {
                    id = staff.StaffId.ToString();
                    role = "Staff";
                }
                //if (user.Status == "Banned" || staff.Status == "Banned")
                //{
                //    return Unauthorized("User has been banned, please contact staff for more info.");
                //}

                // ✅ Tạo JWT token
                Console.WriteLine("Generating JWT token");
                string jwtToken = GenerateJwtToken(id, email, role);
                var refreshToken = GenerateRefreshToken();

                // ✅ Lưu token vào Redis
                Console.WriteLine("Saving token to Redis");
                await _redisService.SetAsync($"refresh:{id}", refreshToken, TimeSpan.FromDays(7));

                Console.WriteLine("Returning JWT token");
                return Ok(new { Token = jwtToken, RefreshToken = refreshToken, Id = id, Email = email, Role = role });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login Error: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest(new { message = "Refresh token is required." });

            // ✅ Kiểm tra Access Token có bị thu hồi không
            var isRevoked = await _redisService.GetAsync($"revoke:{request.AccessToken}");
            if (!string.IsNullOrEmpty(isRevoked))
            {
                return Unauthorized(new { message = "Access Token has been revoked." });
            }

            // ✅ Lấy refresh token từ Redis
            var storedRefreshToken = await _redisService.GetAsync($"refresh:{request.UserId}");
            if (storedRefreshToken != request.RefreshToken)
                return Unauthorized(new { message = "Invalid refresh token." });

            // ✅ Xóa refresh token cũ khỏi Redis
            await _redisService.DeleteAsync($"refresh:{request.UserId}");

            // ✅ Tạo Access Token mới
            var user = await _userService.GetUserById(request.UserId);
            if (user == null) return Unauthorized(new { message = "User not found." });

            string jwtToken = GenerateJwtToken(user.UserId.ToString(), user.Email, user.Role.Name);

            // ✅ Tạo Refresh Token mới
            string newRefreshToken = GenerateRefreshToken();

            // ✅ Lưu refresh token mới vào Redis (hết hạn sau 7 ngày)
            await _redisService.SetAsync($"refresh:{request.UserId}", newRefreshToken, TimeSpan.FromDays(7));

            return Ok(new { Token = jwtToken, RefreshToken = newRefreshToken });
        }



        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto request)
        {
            // Xoá Refresh Token trong Redis để không thể refresh nữa
            //await _redisService.DeleteAsync($"refresh:{request.UserId}");

            // Đánh dấu Access Token đã bị thu hồi
            await _redisService.SetAsync($"revoke:{request.AccessToken}", "true", TimeSpan.FromHours(3));

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet]
        [Authorize]
        public IActionResult TestAuth()
        {
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            return Ok(new { Message = "Authenticated!", Claims = claims });
        }


    }
}
