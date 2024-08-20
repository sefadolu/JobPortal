using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.MVC.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    public class JobSeekerProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JobDbContext _context;
        private readonly IWebHostEnvironment _environment; 

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

            var jobSeeker = _context.JobSeekers
                .Include(js => js.Educations)
                .Include(js => js.Certifications)
                .FirstOrDefault(js => js.Email == user.Email);

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

            var jobSeeker = _context.JobSeekers
                .Include(js => js.Educations)
                .Include(js => js.Certifications)
                .FirstOrDefault(js => js.Email == user.Email);

            if (jobSeeker == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            return View(jobSeeker);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(JobSeeker model, IFormFile resumeFile, IFormFile profilePictureFile, string DeletedEducations, string DeletedCertifications)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "JobSeekerLogin");
            }

            var jobSeeker = _context.JobSeekers
                .Include(js => js.Educations)
                .Include (js => js.Certifications)
                .FirstOrDefault(js => js.Email == user.Email);

            if (jobSeeker == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            // Silinen Eğitimleri kaldır
            if (!string.IsNullOrEmpty(DeletedEducations))
            {
                var educationIds = DeletedEducations.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                var educationsToDelete = jobSeeker.Educations.Where(e => educationIds.Contains(e.Id)).ToList();
                _context.Educations.RemoveRange(educationsToDelete);
            }

            // Silinen Sertifikaları kaldır
            if (!string.IsNullOrEmpty(DeletedCertifications))
            {
                var certificationIds = DeletedCertifications.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                var certificationsToDelete = jobSeeker.Certifications.Where(c => certificationIds.Contains(c.Id)).ToList();
                _context.Certifications.RemoveRange(certificationsToDelete);
            }

            // Formdan gelen alanları doğrudan güncelle
            jobSeeker.FirstName = !string.IsNullOrEmpty(model.FirstName) ? model.FirstName : jobSeeker.FirstName;
            jobSeeker.LastName = !string.IsNullOrEmpty(model.LastName) ? model.LastName : jobSeeker.LastName;
            jobSeeker.PhoneNumber = !string.IsNullOrEmpty(model.PhoneNumber) ? model.PhoneNumber : jobSeeker.PhoneNumber;
            jobSeeker.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : jobSeeker.Address;
            jobSeeker.Skills = !string.IsNullOrEmpty(model.Skills) ? model.Skills : jobSeeker.Skills;

            // Eğitim bilgilerini güncelle
            if (model.Educations != null && model.Educations.Any())
            {
                foreach (var education in model.Educations)
                {
                    var existingEducation = jobSeeker.Educations?.FirstOrDefault(e => e.Id == education.Id);
                    if (existingEducation != null)
                    {
                        existingEducation.SchoolName = education.SchoolName;
                        existingEducation.Department = education.Department;
                        existingEducation.GraduationDegree = education.GraduationDegree;
                        existingEducation.Status = education.Status;
                        existingEducation.GraduationYear = education.GraduationYear;
                    }
                    else
                    {
                        jobSeeker.Educations.Add(education);
                    }
                }
            }

            // Sertifika bilgilerini güncelle
            if (model.Certifications != null && model.Certifications.Any())
            {
                foreach (var certification in model.Certifications)
                {
                    var existingCertification = jobSeeker.Certifications?.FirstOrDefault(c => c.Id == certification.Id);
                    if (existingCertification != null)
                    {
                        existingCertification.InstitutionName = certification.InstitutionName;
                        existingCertification.CertificateName = certification.CertificateName;
                        existingCertification.CertificationDate = certification.CertificationDate;
                    }
                    else
                    {
                        jobSeeker.Certifications.Add(certification);
                    }
                }
            }

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
                await _signInManager.RefreshSignInAsync(user); 
                return RedirectToAction("Profile", new { Message = "Şifreniz başarıyla değiştirildi." });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }
        [Authorize(Roles = "JobSeeker")]
        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "JobSeekerLogin");
            }

            var jobSeeker = await _context.JobSeekers
                .Include(js => js.Educations)
                .Include(js => js.Certifications)
                .FirstOrDefaultAsync(js => js.Email == user.Email);

            if (jobSeeker == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            // İlgili JobSeeker ve AspNetUser kaydını sil
            _context.JobSeekers.Remove(jobSeeker);
            await _context.SaveChangesAsync();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "JobSeekerLogin", new { Message = "Hesabınız başarıyla silindi." });
            }

            return RedirectToAction("Profile", new { Message = "Hesabınız silinirken bir hata oluştu." });
        }
    }
}
