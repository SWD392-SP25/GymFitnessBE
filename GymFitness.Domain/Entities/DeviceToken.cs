using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class DeviceToken
{
    public Guid Id { get; set; }

    public string? DeviceToken1 { get; set; }
}
