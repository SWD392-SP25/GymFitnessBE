namespace GymFitness.API.RequestDto
{
    public class UserSubscriptionRequestDto
    {
        public int SubscriptionPlanId { get; set; }
        public string PaymentFrequency { get; set; } = "Monthly";
        public bool AutoRenew { get; set; } = true;
    }
}
