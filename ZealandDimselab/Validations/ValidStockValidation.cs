//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;
//using ZealandDimselab.Models;

//namespace ZealandDimselab.Validations
//{
//    public class ValidStockValidation : ValidationAttribute
//    {

//        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
//        {
//            if (value > ((Item)validationContext.Items.First())
//            {
//                return new ValidationResult("Not enough items in stock for Booking.");
//            }
//            return ValidationResult.Success;
//        }
//    }
//}