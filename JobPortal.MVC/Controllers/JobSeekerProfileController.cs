using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.MVC.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    public class JobSeekerProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JobDbContext _context;
        private readonly IWebHostEnvironment _environment; // Dosya kaydetme için gerekli

        public JobSeekerProfileController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, JobDbContext context, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _environment = environment;
        }

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

        [HttpPost]
        public async Task<IActionResult> EditProfile(JobSeeker model, IFormFile resumeFile, IFormFile profilePictureFile)
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

            // Formdan gelen alanları doğrudan güncelle
            jobSeeker.FirstName = !string.IsNullOrEmpty(model.FirstName) ? model.FirstName : jobSeeker.FirstName;
            jobSeeker.LastName = !string.IsNullOrEmpty(model.LastName) ? model.LastName : jobSeeker.LastName;
            jobSeeker.PhoneNumber = !string.IsNullOrEmpty(model.PhoneNumber) ? model.PhoneNumber : jobSeeker.PhoneNumber;
            jobSeeker.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : jobSeeker.Address;
            jobSeeker.Skills = !string.IsNullOrEmpty(model.Skills) ? model.Skills : jobSeeker.Skills;

            // Eğer bir özgeçmiş dosyası yüklendiyse
            if (resumeFile != null && resumeFile.Length > 0)
            {
                var resumeFolder = Path.Combine(_environment.WebRootPath, "resumes");
                var resumeFileName = $"{jobSeeker.FirstName}_{jobSeeker.LastName}_{Path.GetFileName(resumeFile.FileName)}";
                var resumePath = Path.Combine(resumeFolder, resumeFileName);

                // Eski dosyayı silme
                if (!string.IsNullOrEmpty(jobSeeker.Resume))
                {
                    var existingResumePath = Path.Combine(resumeFolder, jobSeeker.Resume);
                    if (System.IO.File.Exists(existingResumePath))
                    {
                        System.IO.File.Delete(existingResumePath);
                    }
                }

                // Dosya dizini yoksa oluştur
                if (!Directory.Exists(resumeFolder))
                {
                    Directory.CreateDirectory(resumeFolder);
                }

                // Dosyayı kaydet
                using (var stream = new FileStream(resumePath, FileMode.Create))
                {
                    await resumeFile.CopyToAsync(stream);
                }

                // Yeni özgeçmiş dosya adını kaydet
                jobSeeker.Resume = resumeFileName;
            }

            // Profil fotoğrafı yüklendiyse
            if (profilePictureFile != null && profilePictureFile.Length > 0)
            {
                var profileFolder = Path.Combine(_environment.WebRootPath, "profilepictures");
                var profileFileName = $"{jobSeeker.FirstName}_{jobSeeker.LastName}_{Path.GetFileName(profilePictureFile.FileName)}";
                var profilePath = Path.Combine(profileFolder, profileFileName);

                // Eski profil fotoğrafını silme
                if (!string.IsNullOrEmpty(jobSeeker.ProfilePicture))
                {
                    var existingProfilePath = Path.Combine(profileFolder, jobSeeker.ProfilePicture);
                    if (System.IO.File.Exists(existingProfilePath))
                    {
                        System.IO.File.Delete(existingProfilePath);
                    }
                }

                // Dosya dizini yoksa oluştur
                if (!Directory.Exists(profileFolder))
                {
                    Directory.CreateDirectory(profileFolder);
                }

                // Yeni dosyayı kaydet
                using (var stream = new FileStream(profilePath, FileMode.Create))
                {
                    await profilePictureFile.CopyToAsync(stream);
                }

                // Yeni profil fotoğrafı dosya adını kaydet
                jobSeeker.ProfilePicture = profileFileName;
            }

            _context.JobSeekers.Update(jobSeeker);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile");
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
                return RedirectToAction("Login", "JobSeekerLogin");
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Yeni şifreler eşleşmiyor.");
                return View();
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user); // Kullanıcı oturumunu güncelle
                return RedirectToAction("Profile", new { Message = "Şifreniz başarıyla değiştirildi." });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }
    }
}
