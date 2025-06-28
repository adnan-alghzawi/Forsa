using Forsa.Models;
using Microsoft.AspNetCore.Mvc;

public class SubscriptionController : Controller
{
    private readonly ForsaDbContext _context;

    public SubscriptionController(ForsaDbContext context)
    {
        _context = context;
    }

    // GET: /Subscription
    public IActionResult Index()
    {
        var plans = _context.SubscriptionPlans.ToList();
        return View(plans);
    }

    // POST: /Subscription/Subscribe/3
    [HttpPost]
    public IActionResult Subscribe(int id)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var user = _context.Users.Find(userId);
        var plan = _context.SubscriptionPlans.Find(id);

        if (user == null || plan == null)
            return NotFound();

        user.SubscriptionPlanId = plan.Id;
        user.SubscriptionEndDate = DateTime.Now.AddDays(plan.DurationInDays);

        _context.SaveChanges();

        TempData["Success"] = "تم الاشتراك بالخطة بنجاح!";
        return RedirectToAction("Index", "MyAccount");
    }
}
