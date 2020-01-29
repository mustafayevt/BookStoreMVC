using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "'Email' xanası boş qala bilməz")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "'Şifrə' xanası boş qala bilməz")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}