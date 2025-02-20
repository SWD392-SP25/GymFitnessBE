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

        public AuthController(IConfiguration configuration, IFirebaseAuthService firebaseAuthService,
                              IUserService userService, IStaffService staffService)
        {
            _configuration = configuration;
            _firebaseAuthService = firebaseAuthService;
            _userService = userService;
            _staffService = staffService;
        }

        // ✅ API tạo token từ Firebase UID
        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken([FromBody] string uid)
        {
            var token = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
            return Ok(new { token });
        }


        //[HttpPost("google-login")]
        //public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestDto request)
        //{
        //    try
        //    {
        //        // Xác thực token với Firebase
        //        var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.IdToken);
        //        var uid = decodedToken.Uid;
        //        var email = decodedToken.Claims["email"].ToString();

        //        // Tạo JWT cho hệ thống
        //        var token = GenerateJwtToken(uid, email);

        //        return Ok(new { token });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Unauthorized(new { message = "Invalid token", error = ex.Message });
        //    }
        //}

        // ✅ API xác thực ID Token từ Client gửi lên
        [HttpPost("verify-token")]
        public async Task<IActionResult> VerifyToken([FromHeader] string Authorization)
        {
            if (string.IsNullOrEmpty(Authorization))
            {
                return Unauthorized("Missing token");
            }

            var idToken = Authorization.Replace("Bearer ", "");
            try
            {
                var decodedToken = await _firebaseAuthService.VerifyIdToken(idToken);
                return Ok(new { uid = decodedToken.Uid, message = "Authenticated" });
            }
            catch
            {
                return Unauthorized("Invalid token");
            }
        }

        private string GenerateJwtToken(string id, string email, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
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
                // ✅ Xác thực ID token từ Firebase
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.IdToken);
                string email = decodedToken.Claims["email"]?.ToString();
                string uid = decodedToken.Uid;

                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized(new { message = "Invalid Firebase token" });
                }

                // ✅ Kiểm tra xem user hoặc staff đã tồn tại trong DB chưa
                var user = await _userService.GetUserByEmail(email);
                var staff = await _staffService.GetStaffByEmail(email);

                if (user == null)
                {
                    // Nếu email chưa tồn tại trong cả 2 bảng -> Tạo User mới
                    user = new User
                    {
                        Email = email,
                      
                    };

                     _userService.AddUser(user);
                }

                // ✅ Xác định đối tượng đăng nhập & lấy thông tin
                string role;
                string id;

                if (staff != null)
                {
                    id = staff.StaffId.ToString();
                    role = "Staff";
                }
                else
                {
                    id = user.UserId.ToString();
                    role = "User";
                }

                // ✅ Tạo JWT token
                string jwtToken = GenerateJwtToken(id, email, role);

                return Ok(new { Token = jwtToken, Id = id, Email = email, Role = role });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
