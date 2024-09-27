using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Validations
{
    public class AgePlusEighteenAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;
            var age = DateTime.Now.Year - date.Year;

            if (age < 18)
            {
                return new ValidationResult("Age must be 18 or above.");
            }

            if (age > 120)
            {
                return new ValidationResult("Age must be 120 or below.");
            }

            return ValidationResult.Success;
        }
    }
}
