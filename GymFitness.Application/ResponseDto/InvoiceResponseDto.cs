using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
    public class InvoiceResponseDto
    {
        public int InvoiceId { get; set; }

        //public Guid? UserId { get; set; }

        //public int? SubscriptionId { get; set; }

        public decimal? Amount { get; set; }

        public string? Status { get; set; }

        public DateOnly? DueDate { get; set; }

        public DateOnly? PaidDate { get; set; }

        //public int? PaymentMethodId { get; set; }
        public string? PaymentMethod { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
