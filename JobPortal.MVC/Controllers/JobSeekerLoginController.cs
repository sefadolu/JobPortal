using JobPortal.MVC.Services;
using JobPortal.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace JobPortal.MVC.Controllers
{
    public class JobSeekerLoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender; // Interface olarak kullanılıyor

        public JobSeekerLoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
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

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Lütfen geçerli bir e-posta adresi girin.");
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "Bu e-posta adresine ait kullanıcı bulunamadı.");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "JobSeekerLogin", new { token, email = user.Email }, Request.Scheme);

            // E-posta gönderimi yapılır
            await _emailSender.SendEmailAsync(user.Email, "Şifre Sıfırlama Talebi",
                $"Lütfen şifrenizi sıfırlamak için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>buraya tıklayın</a>.");

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return RedirectToAction("Login"); // Eğer token ya da email null ise login sayfasına yönlendirme
            }
            var model = new ResetPasswordViewModel { Token = token, Email = email }; // Email ve token'i model'e aktar
            return View(model); // ResetPassword sayfasına yönlendir
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) // Form doğrulaması başarısızsa
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) // Eğer kullanıcı bulunamazsa
            {
                // Kullanıcı bulunamıyorsa, direkt Login sayfasına yönlendir
                return RedirectToAction("Login");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded) // Şifre sıfırlama başarılıysa
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            // Hata durumunda hataları göster
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
