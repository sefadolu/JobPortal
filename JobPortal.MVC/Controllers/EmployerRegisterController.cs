using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.MVC.Controllers
{
    public class EmployerRegisterController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JobDbContext _context;

        public EmployerRegisterController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, JobDbContext context)
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
        public async Task<IActionResult> Register(string name, string email, string password, string confirmPassword, string phoneNumber)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Şifreler eşleşmiyor.");
                return View();
            }

            try
            {
                var user = new IdentityUser { UserName = email, Email = email };
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var employer = new Employer
                    {
                        Name = name,
                        Email = email,
                        CompanyProfile = new CompanyProfile { CompanyName = name, PhoneNumber = phoneNumber }
                    };

                    _context.Employers.Add(employer);
                    await _context.SaveChangesAsync();
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Kayıt sırasında bir hata oluştu: {ex.Message}");
            }

            return View();
        }
    }
}
