namespace GymFitness.Application.Dtos
{
    public class AppointmentTypeDto
    {
        public int TypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? DurationMinutes { get; set; }
        public decimal? Price { get; set; }
    }
}
