using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DDraw
{
    class ImageSizeValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Image size cannot be empty.");
            else
            {
                int x;
                if (!Int32.TryParse(value.ToString(), out x))
                {
                    return new ValidationResult(false, "Size must be a number.");
                }
                if (x < 0)
                {
                    return new ValidationResult(false, "Size must be a positive number.");
                }
                if (x > 32000)
                {
                    return new ValidationResult(false, "Size must be less than 32K");
                }
            }
            return ValidationResult.ValidResult;
        }
    }

    class ImageNameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Image size cannot be empty.");
            else
            {
                if (value.ToString().Length >= 255 || value.ToString().Length < 3)
                {
                    return new ValidationResult(false, "Name must be 3-255 characters long.");
                } 
            }
            return ValidationResult.ValidResult;
        }
    }
}
