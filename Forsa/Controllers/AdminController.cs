using Forsa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forsa.Controllers
{
    public class AdminController : Controller
    {
        private readonly ForsaDbContext _context;

        public AdminController(ForsaDbContext context)
        {
            _context = context;
        }

        // عرض كل الجهات المانحة
        public IActionResult Donors(string status = "all")
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return Unauthorized();

            var donors = _context.DonorOrganizations
                .Include(d => d.FundingPrograms)
                .AsQueryable();

            if (status == "active")
                donors = donors.Where(d => !d.IsDeleted);
            else if (status == "deleted")
                donors = donors.Where(d => d.IsDeleted);

            ViewBag.CurrentFilter = status;
            return View(donors.ToList());
        }


        // صفحة إنشاء جهة جديدة
        public IActionResult CreateDonor()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return Unauthorized();

            return View();
        }

        // POST: إنشاء جهة جديدة
        [HttpPost]
        public IActionResult CreateDonor(DonorOrganization donor)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return Unauthorized();

            if (!ModelState.IsValid)
                return View(donor);

            _context.DonorOrganizations.Add(donor);
            _context.SaveChanges();

            return RedirectToAction("Donors");
        }





        public IActionResult Programs(string status = "all")
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return Unauthorized();

            var programs = _context.FundingPrograms
                .Include(p => p.DonorOrganization)
                .AsQueryable();

            if (status == "active")
                programs = programs.Where(p => !p.IsDeleted);
            else if (status == "deleted")
                programs = programs.Where(p => p.IsDeleted);

            ViewBag.CurrentFilter = status;
            return View(programs.ToList());
        }

        [HttpGet]
        public IActionResult AddProgram()
        {
            ViewBag.Donors = _context.DonorOrganizations.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddProgram(FundingProgram program)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Donors = _context.DonorOrganizations.ToList();
                return View(program);
            }

            program.IsDeleted = false;
            _context.FundingPrograms.Add(program);
            _context.SaveChanges();

            return RedirectToAction("Programs");
        }
        [HttpGet]
        public IActionResult EditProgram(int id)
        {
            var program = _context.FundingPrograms.Find(id);
            if (program == null) return NotFound();

            ViewBag.Donors = _context.DonorOrganizations.ToList();
            return View(program);
        }

        [HttpPost]
        public IActionResult EditProgram(FundingProgram program)
        {
            _context.FundingPrograms.Update(program);
            _context.SaveChanges();

            return RedirectToAction("Programs");
        }
        [HttpPost]
        public IActionResult SoftDeleteProgram(int id)
        {
            var program = _context.FundingPrograms.Find(id);
            if (program == null) return NotFound();

            program.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction("Programs");
        }
        [HttpPost]
        public IActionResult ToggleProgramDelete(int id)
        {
            var program = _context.FundingPrograms.Find(id);
            if (program == null) return NotFound();

            program.IsDeleted = !program.IsDeleted; // ✅ عكس الحالة
            _context.SaveChanges();

            return RedirectToAction("Programs");
        }
        [HttpPost]
        public IActionResult ToggleDonorDelete(int id)
        {
            var donor = _context.DonorOrganizations.Find(id);
            if (donor == null) return NotFound();

            donor.IsDeleted = !donor.IsDeleted;
            _context.SaveChanges();

            return RedirectToAction("Donors");
        }

    }

}
