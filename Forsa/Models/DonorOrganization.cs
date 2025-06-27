using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Forsa.Models;

public partial class DonorOrganization
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? ImplementingAgency { get; set; }

    [StringLength(255)]
    public string? LocalPartner { get; set; }

    [StringLength(100)]
    public string? Country { get; set; }

    public string? Notes { get; set; }

    public string? WebsiteLink { get; set; }

    [InverseProperty("DonorOrganization")]
    public virtual ICollection<FundingProgram> FundingPrograms { get; set; } = new List<FundingProgram>();
}
