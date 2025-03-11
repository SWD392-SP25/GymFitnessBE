using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;
public partial class UserSubscription
{
    public int SubscriptionId { get; set; }

    public Guid? UserId { get; set; }

    public int? PlanId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Status { get; set; } = null!;

    public string? PaymentFrequency { get; set; }

    public bool? AutoRenew { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual SubscriptionPlan? Plan { get; set; }

    public virtual User? User { get; set; }
}
