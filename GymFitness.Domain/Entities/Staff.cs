using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GymFitness.Domain.Entities;

public partial class Staff
{
    public int StaffId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? RoleId { get; set; }

    public string? Phone { get; set; }

    public DateOnly? HireDate { get; set; }

    public DateOnly? TerminationDate { get; set; }

    public decimal? Salary { get; set; }

    public string? Status { get; set; }

    public string? Department { get; set; }

    public int? SupervisorId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    [JsonIgnore]
    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    [JsonIgnore]
    public virtual ICollection<ChatHistory> ChatHistories { get; set; } = new List<ChatHistory>();
    [JsonIgnore]
    public virtual ICollection<Staff> InverseSupervisor { get; set; } = new List<Staff>();
    [JsonIgnore]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    [JsonIgnore]
    public virtual Role? Role { get; set; }
    [JsonIgnore]
    public virtual ICollection<StaffSchedule> StaffSchedules { get; set; } = new List<StaffSchedule>();
    [JsonIgnore]
    public virtual ICollection<StaffSpecialization> StaffSpecializations { get; set; } = new List<StaffSpecialization>();
    [JsonIgnore]
    public virtual Staff? Supervisor { get; set; }
    [JsonIgnore]
    public virtual ICollection<SystemSetting> SystemSettings { get; set; } = new List<SystemSetting>();
    [JsonIgnore]
    public virtual ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
}
