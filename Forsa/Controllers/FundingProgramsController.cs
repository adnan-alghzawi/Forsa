using Microsoft.AspNetCore.Mvc;
using Forsa.Models;
using Microsoft.EntityFrameworkCore;

public class FundingProgramsController : Controller
{
    private readonly ForsaDbContext _context;

    public FundingProgramsController(ForsaDbContext context)
    {
        _context = context;
    }

    // GET: /FundingPrograms
    public IActionResult Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var user = _context.Users.FirstOrDefault(u => u.Id == userId);

        if (user == null || user.SubscriptionEndDate == null || user.SubscriptionEndDate < DateTime.Now)
        {
            TempData["Error"] = "انتهت صلاحية اشتراكك. يرجى الاشتراك من جديد.";
            return RedirectToAction("Index", "Subscription");
        }

        var programs = _context.FundingPrograms
            .Include(p => p.DonorOrganization)
            .ToList();

        var favoriteIds = _context.Favorites
            .Where(f => f.UserId == userId)
            .Select(f => f.FundingProgramId)
            .ToList();

        ViewBag.FavoriteIds = favoriteIds;
        ViewBag.UserId = userId;

        return View(programs);
    }


    // AJAX: Toggle Favorite
    [HttpPost]
    public IActionResult ToggleFavorite(int programId)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return Json(new { success = false, message = "Unauthorized" });

        var fav = _context.Favorites
            .FirstOrDefault(f => f.UserId == userId && f.FundingProgramId == programId);

        if (fav != null)
        {
            _context.Favorites.Remove(fav);
        }
        else
        {
            _context.Favorites.Add(new Favorite
            {
                UserId = userId.Value,
                FundingProgramId = programId,
                CreatedAt = DateTime.Now
            });
        }

        _context.SaveChanges();
        return Json(new { success = true });
    }
}
