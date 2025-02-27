using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class StaffSchedule
{
    public int ScheduleId { get; set; }

    public Guid? StaffId { get; set; }

    public int? DayOfWeek { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public bool? IsAvailable { get; set; }

    public string? Location { get; set; }

    public virtual Staff? Staff { get; set; }
}
