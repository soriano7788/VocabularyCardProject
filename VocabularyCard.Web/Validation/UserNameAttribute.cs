using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VocabularyCard.Web.Validation
{
    // ValidationAttribute 為 server 端驗證
    // IClientValidatable 為 client 端驗證
    public class UserNameAttribute : ValidationAttribute, IClientValidatable
    {
        // 無效的名稱
        public string InvalidName { get; set; }

        public override bool IsValid(object value)
        {
            if (Convert.ToString(value).ToLowerInvariant() == InvalidName.ToLowerInvariant())
            {
                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return name + "為無效的使用者名稱";
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("invalidname", InvalidName);
            rule.ValidationType = "displayname";

            yield return rule;
        }
    }
}