using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GymFitness.Domain.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? StaffId { get; set; }

    public int? TypeId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public string? Location { get; set; }

    public DateTime? CreatedAt { get; set; }

    [JsonIgnore]
    public virtual Staff? Staff { get; set; }

    [JsonIgnore]
    public virtual AppointmentType? Type { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }
}
