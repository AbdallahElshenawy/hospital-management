using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.ViewModels
{
    public class ResetPasswordViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Token { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
