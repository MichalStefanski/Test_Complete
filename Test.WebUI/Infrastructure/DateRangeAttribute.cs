using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Test.WebUI.Infrastructure
{
    public class DateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime today = DateTime.Now;
            DateTime min = Convert.ToDateTime("2019-01-01");
            if (Convert.ToDateTime(value) >= min && Convert.ToDateTime(value) <= today)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date is not in given range.");
            }
        }
    }
}