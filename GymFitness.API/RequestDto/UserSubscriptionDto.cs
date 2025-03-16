namespace GymFitness.API.RequestDto
{
    public class UserSubscriptionDto
    {
        public int? SubscriptionPlanId { get; set; } 
        public DateOnly StartDate { get; set; } 
        public DateOnly EndDate { get; set; }
        public string? Status { get; set; } 
        public string? PaymentFrequency { get; set; } 
        public bool? AutoRenew { get; set; }
    }
}
