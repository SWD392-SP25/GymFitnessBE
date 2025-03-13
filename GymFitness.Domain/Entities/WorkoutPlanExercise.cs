using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class WorkoutPlanExercise
{
    public int PlanId { get; set; }

    public int ExerciseId { get; set; }

    public int WeekNumber { get; set; }

    public int DayOfWeek { get; set; }

    public int? Sets { get; set; }

    public int? Reps { get; set; }

    public int? RestTimeSeconds { get; set; }

    public string? Notes { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual WorkoutPlan Plan { get; set; } = null!;
}
