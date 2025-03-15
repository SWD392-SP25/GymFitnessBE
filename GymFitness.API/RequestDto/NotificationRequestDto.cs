namespace GymFitness.API.Dto
{
    public class NotificationRequestDto
    {
        //public Guid? UserId { get; set; }
        //public Guid? StaffId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string? DeviceToken { get; set; }
    }
}