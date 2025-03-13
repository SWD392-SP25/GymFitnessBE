using System;
using System.Collections.Generic;

namespace SeedingDataProject.Entities;

public partial class RegisteredDevice
{
    public int NotificationId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? StaffId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public string? Type { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual User? User { get; set; }
}
