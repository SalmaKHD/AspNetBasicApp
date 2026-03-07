using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FirstWebApplication.Core.Helpers
{
    internal class ValidationHelper
    {
        internal static void ValidateDto(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(obj, validationContext, validationResults, true))
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
