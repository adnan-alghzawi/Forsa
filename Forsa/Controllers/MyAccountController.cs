using Microsoft.AspNetCore.Mvc;
using Forsa.Models;
using Microsoft.EntityFrameworkCore;

public class MyAccountController : Controller
{
    private readonly ForsaDbContext _context;

    public MyAccountController(ForsaDbContext context)
    {
        _context = context;
    }

    // GET: /MyAccount
    public IActionResult Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        return View(user);
    }

    // GET: /MyAccount/Favorites
    public IActionResult Favorites()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var favorites = _context.Favorites
            .Include(f => f.FundingProgram)
            .Where(f => f.UserId == userId)
            .Select(f => f.FundingProgram)
            .ToList();

        return View(favorites);
    }

    // GET: /MyAccount/Notifications
    public IActionResult Notifications()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var notifications = _context.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToList();

        return View(notifications);
    }
}
