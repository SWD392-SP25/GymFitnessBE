using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class WorkoutPlan
{
    public int PlanId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? DifficultyLevel { get; set; }

    public int? DurationWeeks { get; set; }

    public Guid? CreatedBy { get; set; }

    public string? TargetAudience { get; set; }

    public string? Goals { get; set; }

    public string? Prerequisites { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? SubscriptionId { get; set; }

    public virtual Staff? CreatedByNavigation { get; set; }

    public virtual SubscriptionPlan? Subscription { get; set; }

    public virtual ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; set; } = new List<WorkoutPlanExercise>();
}
