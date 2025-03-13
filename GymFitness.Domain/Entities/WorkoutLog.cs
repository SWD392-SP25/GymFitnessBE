using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class WorkoutLog
{
    public int LogId { get; set; }

    public Guid? UserId { get; set; }

    public int? ExerciseId { get; set; }

    public DateOnly? Date { get; set; }

    public int? SetsCompleted { get; set; }

    public int? RepsCompleted { get; set; }

    public decimal? WeightUsed { get; set; }

    public int? DurationMinutes { get; set; }

    public int? DifficultyRating { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Exercise? Exercise { get; set; }

    public virtual User? User { get; set; }
}
