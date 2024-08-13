using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.MVC.Controllers
{
    public class EmployerController : Controller
    {
        private readonly JobDbContext _context;

        public EmployerController(JobDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Kayıt formu yüklenir
        }

        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string password, string confirmPassword, string phoneNumber)
        {

            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("", "Şirket adı zorunludur.");
                return View();
            }

            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Şifreler eşleşmiyor.");
                return View();
            }

            var employer = new Employer
            {
                Name = name,
                Email = email,
                Password = password,
                CompanyProfile = new CompanyProfile { CompanyName = name, PhoneNumber = phoneNumber }
            };

            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
