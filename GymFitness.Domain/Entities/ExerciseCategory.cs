using System;
using System.Collections.Generic;

namespace SeedingDataProject.Entities;

public partial class ExerciseCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
