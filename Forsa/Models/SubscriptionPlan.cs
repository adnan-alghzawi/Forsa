using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Forsa.Models;

public partial class SubscriptionPlan
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    public int DurationInDays { get; set; }

    public string? Description { get; set; }

    [InverseProperty("SubscriptionPlan")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
