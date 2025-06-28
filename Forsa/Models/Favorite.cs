using System;
using System.Collections.Generic;

namespace Forsa.Models;

public partial class Favorite
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FundingProgramId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual FundingProgram FundingProgram { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
