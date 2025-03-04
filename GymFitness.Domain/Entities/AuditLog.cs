using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

//ghi lại lịch sử hoạt động
public partial class AuditLog
{
    public int LogId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? StaffId { get; set; }

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
