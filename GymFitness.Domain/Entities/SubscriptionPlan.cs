using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class SubscriptionPlan
{
    public int PlanId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int DurationMonths { get; set; }

    public string? Features { get; set; }

    public int? MaxSessionsPerMonth { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
