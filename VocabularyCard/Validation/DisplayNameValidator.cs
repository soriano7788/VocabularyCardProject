using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VocabularyCard.Validation
{
    public class DisplayNameValidator
    {
        public static ValidationResult InValid(string displayName, ValidationContext validationContext)
        {
            Regex regex = new Regex(@"[^\w\.-]", RegexOptions.IgnoreCase);
            return (displayName != null && regex.Match(displayName).Length > 0)
                ? new ValidationResult("只允許含英數字元，句號(.)，連字號(-)。")
                : ValidationResult.Success;
        }

        public static ValidationResult IsNotEmpty(string displayName, ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(displayName))
            {
                return new ValidationResult("字串不得為空");
            }
            return ValidationResult.Success;
        }

    }
}
