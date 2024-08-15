using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.MVC.Controllers
{
    public class JobSeekerProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JobDbContext _context;

        public JobSeekerProfileController(UserManager<IdentityUser> userManager, JobDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // Profili görüntüleme
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "JobSeekerLogin");
            }

            var jobSeeker = _context.JobSeekers.FirstOrDefault(js => js.Email == user.Email);
            if (jobSeeker == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            return View(jobSeeker);
        }

        // Profili düzenleme sayfası
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "JobSeekerLogin");
            }

            var jobSeeker = _context.JobSeekers.FirstOrDefault(js => js.Email == user.Email);
            if (jobSeeker == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            return View(jobSeeker);
        }

        // Profili düzenleme işlemi
        [HttpPost]
        public async Task<IActionResult> EditProfile(JobSeeker model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "JobSeekerLogin");
            }

            var jobSeeker = _context.JobSeekers.FirstOrDefault(js => js.Email == user.Email);
            if (jobSeeker == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            // Kullanıcının bilgilerini güncelle
            jobSeeker.FirstName = model.FirstName;
            jobSeeker.LastName = model.LastName;
            jobSeeker.PhoneNumber = model.PhoneNumber;
            jobSeeker.Address = model.Address;
            jobSeeker.Resume = model.Resume;
            jobSeeker.Skills = model.Skills;

            _context.JobSeekers.Update(jobSeeker);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile");
        }
    }
}
