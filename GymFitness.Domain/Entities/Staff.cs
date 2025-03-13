using System;
using System.Collections.Generic;

namespace SeedingDataProject.Entities;

public partial class Staff
{
    public Guid StaffId { get; set; }

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? RoleId { get; set; }

    public string? Phone { get; set; }

    public DateOnly? HireDate { get; set; }

    public DateOnly? TerminationDate { get; set; }

    public decimal? Salary { get; set; }

    public string? Status { get; set; }

    public string? Department { get; set; }

    public Guid? SupervisorId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<ChatHistory> ChatHistories { get; set; } = new List<ChatHistory>();

    public virtual ICollection<Staff> InverseSupervisor { get; set; } = new List<Staff>();

    public virtual ICollection<RegisteredDevice> RegisteredDevices { get; set; } = new List<RegisteredDevice>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<StaffSpecialization> StaffSpecializations { get; set; } = new List<StaffSpecialization>();

    public virtual Staff? Supervisor { get; set; }

    public virtual ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
}
