using System;
using System.Collections.Generic;

namespace GymFitness.Infrastructure.Data;

public partial class AuditLog
{
    public int LogId { get; set; }

    public int? UserId { get; set; }

    public int? StaffId { get; set; }

    public string? Action { get; set; }

    public string? TableName { get; set; }

    public int? RecordId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? IpAddress { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual User? User { get; set; }
}
