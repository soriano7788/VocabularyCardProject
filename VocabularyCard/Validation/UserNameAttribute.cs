using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Validation
{
    public class UserNameAttribute : ValidationAttribute
    {
        // 無效的名稱
        public string InvalidName { get; set; }

        public override bool IsValid(object value)
        {
            if(Convert.ToString(value).ToLowerInvariant() == InvalidName.ToLowerInvariant())
            {
                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return name + "為無效的使用者名稱"; 
        }
    }
}
