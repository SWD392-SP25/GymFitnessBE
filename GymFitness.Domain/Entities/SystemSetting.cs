using System;
using System.Collections.Generic;

namespace GymFitness.Infrastructure.Data;

public partial class SystemSetting
{
    public int SettingId { get; set; }

    public string SystemKey { get; set; } = null!;

    public string? Value { get; set; }

    public string? Description { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Staff? UpdatedByNavigation { get; set; }
}
