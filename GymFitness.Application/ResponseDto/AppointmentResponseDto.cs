namespace GymFitness.Application.ResponseDto
{
    public class AppointmentResponseDto
    {
        //public int AppointmentId { get; set; }
        public string? UserName { get; set; } // Lấy từ bảng User
        public string? StaffName { get; set; } // Lấy từ bảng Staff
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public string? Location { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; } // Lấy từ bảng AppointmentType
        public DateTime? CreatedAt { get; set; }
    }
}
