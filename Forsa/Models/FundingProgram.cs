using System;
using System.Collections.Generic;

namespace Forsa.Models;

public partial class FundingProgram
{
    public int Id { get; set; }

    public int DonorOrganizationId { get; set; }

    public string ProgramName { get; set; } = null!;

    public string? Sector { get; set; }

    public string? TargetRegion { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? MinAmount { get; set; }

    public decimal? MaxAmount { get; set; }

    public string? FundingType { get; set; }

    public string? EligibilityCriteria { get; set; }

    public string? ApplicationLink { get; set; }

    public string? Notes { get; set; }

    public bool IsDeleted { get; set; }

    public virtual DonorOrganization DonorOrganization { get; set; } = null!;

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
