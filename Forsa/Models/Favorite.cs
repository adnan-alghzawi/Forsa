using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Forsa.Models;

public partial class Favorite
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FundingProgramId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("FundingProgramId")]
    [InverseProperty("Favorites")]
    public virtual FundingProgram FundingProgram { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Favorites")]
    public virtual User User { get; set; } = null!;
}
