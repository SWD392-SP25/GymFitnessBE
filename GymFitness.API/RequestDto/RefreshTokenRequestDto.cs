namespace GymFitness.API.Dto
{
    public class RefreshTokenRequestDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; } // Xác định chủ sở hữu token
    }
}
