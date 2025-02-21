using System;
using System.Collections.Generic;

namespace GymFitness.Infrastructure.Data;

public partial class StaffSpecialization
{
    public int StaffId { get; set; }

    public int SpecializationId { get; set; }

    public string? CertificationNumber { get; set; }

    public DateOnly? CertificationDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string? VerificationStatus { get; set; }

    public virtual Specialization Specialization { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
