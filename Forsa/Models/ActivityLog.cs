﻿using System;
using System.Collections.Generic;

namespace Forsa.Models;

public partial class ActivityLog
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Action { get; set; }

    public string? Details { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
