namespace GymFitness.API.Dto
{
    public class LogoutRequestDto
    {
        public Guid UserId { get; set; }       // ID của người dùng
        public string AccessToken { get; set; }  // Token hiện tại (optional)
        public string RefreshToken { get; set; } // Refresh Token cần thu hồi
    }
}
