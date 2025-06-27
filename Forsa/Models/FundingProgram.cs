using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Forsa.Models;

public partial class FundingProgram
{
    [Key]
    public int Id { get; set; }

    public int DonorOrganizationId { get; set; }

    [StringLength(255)]
    public string ProgramName { get; set; } = null!;

    [StringLength(100)]
    public string? Sector { get; set; }

    [StringLength(100)]
    public string? TargetRegion { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? MinAmount { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? MaxAmount { get; set; }

    [StringLength(100)]
    public string? FundingType { get; set; }

    public string? EligibilityCriteria { get; set; }

    public string? ApplicationLink { get; set; }

    public string? Notes { get; set; }

    [ForeignKey("DonorOrganizationId")]
    [InverseProperty("FundingPrograms")]
    public virtual DonorOrganization DonorOrganization { get; set; } = null!;

    [InverseProperty("FundingProgram")]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
