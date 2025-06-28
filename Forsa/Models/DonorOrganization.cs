using System;
using System.Collections.Generic;

namespace Forsa.Models;

public partial class DonorOrganization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ImplementingAgency { get; set; }

    public string? LocalPartner { get; set; }

    public string? Country { get; set; }

    public string? Notes { get; set; }

    public string? WebsiteLink { get; set; }

    public virtual ICollection<FundingProgram> FundingPrograms { get; set; } = new List<FundingProgram>();
}
