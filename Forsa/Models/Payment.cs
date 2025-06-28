using System;
using System.Collections.Generic;

namespace Forsa.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? StripeSessionId { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    public string? Status { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual User User { get; set; } = null!;
}
