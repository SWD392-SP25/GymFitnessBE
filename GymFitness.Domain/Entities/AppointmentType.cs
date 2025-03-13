using System;
using System.Collections.Generic;

namespace SeedingDataProject.Entities;

public partial class AppointmentType
{
    public int TypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? DurationMinutes { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
