using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.MVC.Controllers
{
    [Authorize(Roles = "Employer")]
    public class EmployerProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JobDbContext _context;

        public EmployerProfileController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, JobDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "EmployerLogin");
            }

            var employer = _context.Employers.Include(e => e.CompanyProfile)
                                             .FirstOrDefault(e => e.Email == user.Email);
            if (employer == null || employer.CompanyProfile == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            return View(employer);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "EmployerLogin");
            }

            var employer = _context.Employers.Include(e => e.CompanyProfile)
                                             .FirstOrDefault(e => e.Email == user.Email);
            if (employer == null || employer.CompanyProfile == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            return View(employer);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(Employer model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "EmployerLogin");
            }

            var employer = _context.Employers.Include(e => e.CompanyProfile)
                                             .FirstOrDefault(e => e.Email == user.Email);
            if (employer == null || employer.CompanyProfile == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            // Formdan gelen alanları güncelle
            employer.CompanyProfile.CompanyName = !string.IsNullOrEmpty(model.CompanyProfile.CompanyName)
                                                  ? model.CompanyProfile.CompanyName
                                                  : employer.CompanyProfile.CompanyName;
            employer.CompanyProfile.PhoneNumber = !string.IsNullOrEmpty(model.CompanyProfile.PhoneNumber)
                                                  ? model.CompanyProfile.PhoneNumber
                                                  : employer.CompanyProfile.PhoneNumber;
            employer.CompanyProfile.Website = !string.IsNullOrEmpty(model.CompanyProfile.Website)
                                              ? model.CompanyProfile.Website
                                              : employer.CompanyProfile.Website;
            employer.CompanyProfile.Location = !string.IsNullOrEmpty(model.CompanyProfile.Location)
                                               ? model.CompanyProfile.Location
                                               : employer.CompanyProfile.Location;
            employer.CompanyProfile.Description = !string.IsNullOrEmpty(model.CompanyProfile.Description)
                                                  ? model.CompanyProfile.Description
                                                  : employer.CompanyProfile.Description;

            _context.Employers.Update(employer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile", new { Message = "Profil başarıyla güncellendi." });
        }

        // Şifre Değiştirme GET
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        // Şifre Değiştirme POST
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "EmployerLogin");
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Yeni şifreler eşleşmiyor.");
                return View();
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Profile", new { Message = "Şifreniz başarıyla değiştirildi." });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }
        [Authorize(Roles = "Employer")]
        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "EmployerLogin");
            }

            var employer = await _context.Employers
                .Include(e => e.CompanyProfile)
                .FirstOrDefaultAsync(e => e.Email == user.Email);

            if (employer == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            _context.Employers.Remove(employer);
            await _context.SaveChangesAsync();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "EmployerLogin", new { Message = "Hesabınız başarıyla silindi." });
            }

            return RedirectToAction("Profile", new { Message = "Hesabınız silinirken bir hata oluştu." });
        }
    }
}
