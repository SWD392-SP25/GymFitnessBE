using System;
using System.Collections.Generic;

namespace GymFitness.Infrastructure.Data;

public partial class StaffSchedule
{
    public int ScheduleId { get; set; }

    public int? StaffId { get; set; }

    public int? DayOfWeek { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public bool? IsAvailable { get; set; }

    public string? Location { get; set; }

    public virtual Staff? Staff { get; set; }
}
