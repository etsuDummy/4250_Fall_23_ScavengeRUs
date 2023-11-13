using ScavengeRUs.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ScavengeRUs.Attributes
{
    public class EndDateDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Hunt)validationContext.ObjectInstance;

            if (model.EndDate < model.StartDate)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
