using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.MVC.Controllers
{
    public class JobSeekerRegisterController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JobDbContext _context;

        public JobSeekerRegisterController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, JobDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var jobSeeker = new JobSeeker
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };

                _context.JobSeekers.Add(jobSeeker);
                await _context.SaveChangesAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }
    }
}