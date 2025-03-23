namespace GymFitness.API.RequestDto
{
    public class InvoiceDto
    {
      

        public string? UserEmail { get; set; }

        public int? SubscriptionId { get; set; }

        public decimal? Amount { get; set; }

        public string? Status { get; set; }

        public DateOnly? DueDate { get; set; }

        public DateOnly? PaidDate { get; set; }

        public int? PaymentMethodId { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
