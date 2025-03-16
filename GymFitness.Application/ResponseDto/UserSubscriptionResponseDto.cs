using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
    public class UserSubscriptionResponseDto
    {
        public int SubscriptionId { get; set; }

        public string UserEmail { get; set; }

        public int? SubscriptionPlanId { get; set; }
        public string? SubscriptionPlanName { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string Status { get; set; } = null!;

        public string? PaymentFrequency { get; set; }

        public bool? AutoRenew { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? Sub { get; set; }
        public List<InvoiceResponseDto> Invoices { get; set; }
    }
}
