using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class PaymentMethod
{
    public int MethodId { get; set; }

    public int? UserId { get; set; }

    public string? Provider { get; set; }

    public string? TokenReference { get; set; }

    public string? LastFour { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public bool? IsDefault { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User? User { get; set; }
}
