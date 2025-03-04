namespace GymFitness.API.Dto
{
    public class SubscriptionPlanDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationMonths { get; set; }
        public string? Features { get; set; }
        public int? MaxSessionsPerMonth { get; set; }
        public bool? IsActive { get; set; }
    }
}
