using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="'Ad' Xanası Boş Qala Bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage=" Boş Qala Bilməz")]
        public string Surname { get; set; }
        [Required(ErrorMessage="'Soyad' Xanası Boş Qala Bilməz")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "Telefon Nömrəsi Formata Uyğun Deyil")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage="'Email' Xanası Boş Qala Bilməz")]
        [EmailAddress(ErrorMessage = "Email Formatı Düzgün Deyil")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage="'Şifrə' Xanası Boş Qala Bilməz")]
        [MinLength(4,ErrorMessage = "Şifrə Minimum 4 Simvol Olmalıdır")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public User ToUser()
        {
            var user = new User();
            user.Email = Email;
            user.Name = Name;
            user.Surname = Surname;
            user.PhoneNumber = PhoneNumber;

            return user;
        }
    }
}