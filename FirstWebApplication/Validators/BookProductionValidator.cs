using System.ComponentModel.DataAnnotations;

namespace FirstWebApplication.Validators
{
    public class BookProductionValidator: ValidationAttribute
    {
        // we may add all  functionalities like {0} attributes or supplying values to validator in custom validators also
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date.Year <= 2000)
                {
                    return new ValidationResult("year must be at least 2000");
                }
                else
                {
                    return ValidationResult.Success;
                }
            } else
            {
               return null;
            }
        }

    }
}
