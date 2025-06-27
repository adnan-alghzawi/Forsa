using Forsa.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

public class AccountController : Controller
{
    private readonly ForsaDbContext _context;

    public AccountController(ForsaDbContext context)
    {
        _context = context;
    }

    // GET: /Account/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    public IActionResult Register(User user)
    {
        if (_context.Users.Any(u => u.Email == user.Email))
        {
            ModelState.AddModelError("Email", "Email already exists");
            return View(user);
        }

        user.PasswordHash = HashPassword(user.PasswordHash);
        user.Role = "User";
        user.CreatedAt = DateTime.Now;

        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }

    // GET: /Account/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var hashedPassword = HashPassword(password);
        var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == hashedPassword);

        if (user == null)
        {
            ViewBag.Error = "Invalid email or password";
            return View();
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.FullName);
        HttpContext.Session.SetString("UserRole", user.Role);

        if (TempData["ReturnUrl"] != null)
            return Redirect(TempData["ReturnUrl"].ToString());

        return RedirectToAction("Index", "Home");
    }


    // Logout
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
