namespace GymFitness.API.Dto
{
    public class StaffScheduleDto
    {
        public Guid? StaffId { get; set; }
        public int? DayOfWeek { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public bool? IsAvailable { get; set; }
        public string? Location { get; set; }
    }
}
