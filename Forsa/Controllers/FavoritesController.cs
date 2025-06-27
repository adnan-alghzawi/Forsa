using Azure.Core;
using Forsa.Models;
using Microsoft.AspNetCore.Mvc;

public class FavoritesController : Controller
{
    private readonly ForsaDbContext _context;

    public FavoritesController(ForsaDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult ToggleFavorite(int programId)
    {
        // التحقق من الجلسة - هل المستخدم مسجل دخول؟
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            // خزّن الصفحة الحالية عشان نرجع لها بعد تسجيل الدخول
            TempData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            return RedirectToAction("Login", "Account");
        }

        int userId = HttpContext.Session.GetInt32("UserId").Value;

        var existing = _context.Favorites
            .FirstOrDefault(f => f.UserId == userId && f.FundingProgramId == programId);

        if (existing != null)
        {
            _context.Favorites.Remove(existing);
        }
        else
        {
            _context.Favorites.Add(new Favorite
            {
                UserId = userId,
                FundingProgramId = programId,
                CreatedAt = DateTime.Now
            });
        }

        _context.SaveChanges();

        return Redirect(Request.Headers["Referer"].ToString());
    }

}
