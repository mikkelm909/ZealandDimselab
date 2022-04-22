using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Validations
{
    public class ValidDateTimeValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            if (dateTime.Date < DateTime.Now.Date)
            {
                return new ValidationResult("Return date cannot be less than today");
            }
            return ValidationResult.Success;
        }
    }
}
