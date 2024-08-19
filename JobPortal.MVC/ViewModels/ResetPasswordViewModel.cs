using System.ComponentModel.DataAnnotations;

namespace JobPortal.MVC.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}
