using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Forsa.Models;

public partial class ActivityLog
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(255)]
    public string? Action { get; set; }

    public string? Details { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ActivityLogs")]
    public virtual User User { get; set; } = null!;
}
