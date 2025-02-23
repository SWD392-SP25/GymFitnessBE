using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? UserId { get; set; }

    public int? StaffId { get; set; }

    public int? TypeId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public string? Location { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual AppointmentType? Type { get; set; }

    public virtual User? User { get; set; }
}
