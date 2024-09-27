using Hospital_Management.Data;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Validations
{
    public class UniquePhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            var phone = value as string;
            var user = context.Users.FirstOrDefault(u => u.PhoneNumber == phone);

            if (user != null)
            {
                return new ValidationResult("Phone number already exists.");
            }

            return ValidationResult.Success;
        }
    }
}
