using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;
public partial class UserMeasurement
{
    public int MeasurementId { get; set; }

    public Guid? UserId { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Height { get; set; }

    public decimal? BodyFatPercentage { get; set; }

    public decimal? ChestCm { get; set; }

    public decimal? WaistCm { get; set; }

    public decimal? HipsCm { get; set; }

    public decimal? ArmsCm { get; set; }

    public decimal? ThighsCm { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
