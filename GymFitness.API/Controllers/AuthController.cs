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

namespace GymFitness.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFirebaseAuthService _firebaseAuthService;

        public AuthController(IConfiguration configuration, IFirebaseAuthService firebaseAuthService)
        {
            _configuration = configuration;
            _firebaseAuthService = firebaseAuthService;
        }

        // ✅ API tạo Firebase Custom Token (chỉ dùng khi cần)
        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken([FromBody] string uid)
        {
            var token = await _firebaseAuthService.GenerateFirebaseToken(uid);
            return Ok(new { token });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestDto request)
        {
            try
            {
                // Xác thực token với Firebase
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.IdToken);
                var uid = decodedToken.Uid;
                var email = decodedToken.Claims["email"].ToString();

                // Tạo JWT cho hệ thống
                var token = GenerateJwtToken(uid, email);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Invalid token", error = ex.Message });
            }
        }

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

        private string GenerateJwtToken(string uid, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, uid),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, "User") // Default role
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
