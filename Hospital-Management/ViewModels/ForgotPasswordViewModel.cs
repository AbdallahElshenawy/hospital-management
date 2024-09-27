using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "You can't leave it empty.")]
        [EmailAddress(ErrorMessage = "Please Enter a valid email address.")]
        public string Email { get; set; }
    }
}
