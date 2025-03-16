namespace GymFitness.API.RequestDto
{
    public class UserSubscriptionCreateDto
    {
        public string Email { get; set; }
        public int SubscriptionPlanId { get; set; }
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string Status { get; set; } = null!;

        public string? PaymentFrequency { get; set; }

        public bool? AutoRenew { get; set; }

        public string? Sub { get; set; }

    }
}
