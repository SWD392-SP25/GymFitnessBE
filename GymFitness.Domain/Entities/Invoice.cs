using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int? UserId { get; set; }

    public int? SubscriptionId { get; set; }

    public decimal? Amount { get; set; }

    public string? Status { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateOnly? PaidDate { get; set; }

    public int? PaymentMethodId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual UserSubscription? Subscription { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User? User { get; set; }
}
