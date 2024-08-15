using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.MVC.Controllers
{
    public class JobSeekerLoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public JobSeekerLoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(); // Giriş formu yüklenir
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
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
