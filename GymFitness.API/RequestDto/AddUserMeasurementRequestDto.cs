namespace GymFitness.API.Dto
{
    public class AddUserMeasurementRequestDto
    {
        public Guid UserId { get; set; }
        public DateOnly Date { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Body_Fat { get; set; }
        public decimal Hips_cm { get; set; }
        public decimal Arm_cm { get; set; }
        public decimal Chest_cm { get; set; }
        public decimal Waist_cm { get; set; }
        public decimal Thigh_cm { get; set; }
    }
}
