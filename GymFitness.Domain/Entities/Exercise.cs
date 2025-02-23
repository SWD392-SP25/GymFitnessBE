using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class Exercise
{
    public int ExerciseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? MuscleGroupId { get; set; }

    public int? CategoryId { get; set; }

    public string? DifficultyLevel { get; set; }

    public string? EquipmentNeeded { get; set; }

    public string? VideoUrl { get; set; }

    public string? ImageUrl { get; set; }

    public string? Instructions { get; set; }

    public string? Precautions { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ExerciseCategory? Category { get; set; }

    public virtual MuscleGroup? MuscleGroup { get; set; }

    public virtual ICollection<WorkoutLog> WorkoutLogs { get; set; } = new List<WorkoutLog>();

    public virtual ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; set; } = new List<WorkoutPlanExercise>();
}
