using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.MVC.Controllers
{
    public class JobSeekerLoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public JobSeekerLoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(); // Giriş formu yüklenir
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                // Kullanıcının "JobSeeker" rolünde olup olmadığını kontrol edelim
                var isJobSeeker = await _userManager.IsInRoleAsync(user, "JobSeeker");
                if (!isJobSeeker)
                {
                    ModelState.AddModelError(string.Empty, "Bu sayfadan sadece iş arayanlar giriş yapabilir.");
                    return View();
                }

                var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Hesabınız kilitlendi. Lütfen daha sonra tekrar deneyin.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Girişe izin verilmiyor. Lütfen email adresinizi doğrulayın.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Giriş başarısız oldu. Lütfen bilgilerinizi kontrol edin ve tekrar deneyin.");
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Bu email adresine ait bir hesap bulunamadı.");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
