using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Forsa.Models;

[Index("Email", Name = "UQ__Users__A9D10534D5D55BC9", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string FullName { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    [StringLength(50)]
    public string? Phone { get; set; }

    [StringLength(100)]
    public string? Country { get; set; }

    [StringLength(50)]
    public string Role { get; set; } = null!;

    public int? SubscriptionPlanId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    [InverseProperty("User")]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    [InverseProperty("User")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("User")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("SubscriptionPlanId")]
    [InverseProperty("Users")]
    public virtual SubscriptionPlan? SubscriptionPlan { get; set; }
}
