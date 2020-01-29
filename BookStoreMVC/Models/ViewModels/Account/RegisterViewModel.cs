using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public User ToUser()
        {
            var user = new User();
            user.Email = Email;
            user.Name = Name;
            user.Surname = Surname;
            user.UserName = Username;
            user.PhoneNumber = PhoneNumber;

            return user;
        }
    }
}