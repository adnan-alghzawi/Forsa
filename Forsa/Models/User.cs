using System;
using System.Collections.Generic;

namespace Forsa.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Country { get; set; }

    public string Role { get; set; } = null!;

    public int? SubscriptionPlanId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? SubscriptionEndDate { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual SubscriptionPlan? SubscriptionPlan { get; set; }
}
