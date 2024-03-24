using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfWalkThrough
{
    class NameValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            object? propertyValue = null;

            // Když je ValidationStep na hodnote UpdatedValue, přenáší se do metody celá 
            // informace o Bindingu a musíme tedy hodnotu "vydolovat" pomocí Reflection
            if (value is BindingExpression)
            {
                var v = value as BindingExpression;

                if (v != null)
                {
                    object dataItem = v.DataItem;
                    string propertyName = v.ParentBinding.Path.Path; // název proměnné, která je navázaná na GUI
                    PropertyInfo? pi = dataItem?.GetType()?.GetProperty(propertyName); // Reflection
                    propertyValue = pi?.GetValue(dataItem);
                }

            }
            else
            // Když je ValidationStep nedefinovaný, do metody se přenáší hodnota vlastnosti přímo
            {
                propertyValue = value;
            }

            // Teď můžeme validovat v obou případech
            if (propertyValue == null || string.IsNullOrEmpty(propertyValue as string))
            {
                return new ValidationResult(false, "Jméno je povinné.");
            }

            if ((propertyValue as string).Length < 3)
            {
                return new ValidationResult(false, "Jméno musí být delší než 2 znaky.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
