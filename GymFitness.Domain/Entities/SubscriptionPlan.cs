using System;
using System.Collections.Generic;

namespace SeedingDataProject.Entities;

public partial class SubscriptionPlan
{
    public int SubscriptionPlanId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int DurationMonths { get; set; }

    public string? Features { get; set; }

    public int? MaxSessionsPerMonth { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();

    public virtual ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
}
