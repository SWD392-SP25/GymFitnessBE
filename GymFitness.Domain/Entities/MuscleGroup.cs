using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class MuscleGroup
{
    public int MuscleGroupId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
