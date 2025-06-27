using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Forsa.Models;

public partial class Payment
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(255)]
    public string? StripeSessionId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Amount { get; set; }

    [StringLength(10)]
    public string? Currency { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PaymentDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Payments")]
    public virtual User User { get; set; } = null!;
}
