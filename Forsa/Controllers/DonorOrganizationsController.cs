using Forsa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DonorOrganizationsController : Controller
{
    private readonly ForsaDbContext _context;

    public DonorOrganizationsController(ForsaDbContext context)
    {
        _context = context;
    }

    // GET: /DonorOrganizations
    public IActionResult Index()
    {
        var donors = _context.DonorOrganizations.ToList();
        return View(donors);
    }

    // GET: /DonorOrganizations/Details/5
    
    public IActionResult Details(int id)
    {
        var donor = _context.DonorOrganizations
            .Include(d => d.FundingPrograms)
            .FirstOrDefault(d => d.Id == id);

        if (donor == null)
            return NotFound();

        return View(donor);
    }

}
