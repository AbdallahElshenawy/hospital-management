using Hospital_Management.Data;
using Hospital_Management.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Hospital_Management.ViewModels
{
    public class RegisterationViewModel
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, ErrorMessage = "First Name can't be longer than 100 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, ErrorMessage = "Last Name can't be longer than 100 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [UniqueEmail(ErrorMessage = "Email already exists.")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [UniquePhoneNumber(ErrorMessage = "Phone number already exists.")]
        [StringLength(20, ErrorMessage = "Phone number can't be longer than 20 characters.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [AgePlusEighteen(ErrorMessage = "Age must be 18 or above.")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address can't be longer than 200 characters.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public IFormFile? Image { get; set; }


        public string GenerateUsername()
        {
            int random = new Random().Next(100, 1000);

            return Email.Split("@")[0] + random;
        }
    }
}
