using System;
using System.Collections.Generic;

namespace GymFitness.Infrastructure.Data;

public partial class Specialization
{
    public int SpecializationId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<StaffSpecialization> StaffSpecializations { get; set; } = new List<StaffSpecialization>();
}
