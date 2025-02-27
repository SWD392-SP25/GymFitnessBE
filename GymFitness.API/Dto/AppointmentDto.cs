namespace GymFitness.Application.Dtos
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? StaffId { get; set; }
        public int? TypeId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public string? Location { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
