using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfWalkThrough
{
    class NameValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrEmpty(value as string)) 
            { 
                return new ValidationResult(false, "Jméno je povinné."); 
            }

            if ((value as string).Length < 3)
            {
                return new ValidationResult(false, "Jméno musí být delší než 2 znaky.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
