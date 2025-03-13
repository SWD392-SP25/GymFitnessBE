using System;
using System.Collections.Generic;

namespace SeedingDataProject.Entities;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? InvoiceId { get; set; }

    public decimal? Amount { get; set; }

    public string? Status { get; set; }

    public int? PaymentMethodId { get; set; }

    public string? TransactionReference { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual PaymentMethod? PaymentMethod { get; set; }
}
