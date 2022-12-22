using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Attributes
{
    public class BeforeOrToday : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime pDate = (DateTime)value;
                return DateTime.Today <= pDate ? new ValidationResult(ErrorMessage ?? "Ngày không được lớn hơn ngày hiện tại.") : ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}
