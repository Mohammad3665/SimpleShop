using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name cannot be blank")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "E-mail cannot be blank")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password cannot be blank")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password dont match")]
        public string ConfirmPassword { get; set; }

    }
}
