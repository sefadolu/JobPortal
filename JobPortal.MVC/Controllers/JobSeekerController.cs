using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.MVC.Controllers
{
    public class JobSeekerController : Controller
    {
        private readonly JobDbContext _context;

        public JobSeekerController(JobDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Kayıt formu yüklenir
        }

        [HttpPost]
        public async Task<IActionResult> Register(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Şifreler eşleşmiyor.");
                return View();
            }

            var jobSeeker = new JobSeeker
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
            
            };

            _context.JobSeekers.Add(jobSeeker);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}