using Hospital_Management.Data;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Validations
{
    public class UniqueEmailAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            var email = value as string;
            var user = context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                return new ValidationResult("Email already exists.");
            }

            return ValidationResult.Success;
        }
    }
}
