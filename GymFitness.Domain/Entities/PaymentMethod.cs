using System;
using System.Collections.Generic;

namespace GymFitness.Domain.Entities;

public partial class PaymentMethod
{
    public int MethodId { get; set; }

    public string? MethodName { get; set; }

    public string? Detail { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
