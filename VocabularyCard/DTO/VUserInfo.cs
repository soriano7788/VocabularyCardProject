using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Validation;

namespace VocabularyCard.DTO
{
    public class VUserInfo : IValidatableObject
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        private string _displayName;
        [UserName(InvalidName = "fuck")]
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        private string _email;
        [Email]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// 提供此 User 資料的來源，例如 google、ldap、facebook 等等
        /// 參考自 .net identity 的 AspNetUserLogins 的 LoginProvider
        /// </summary>
        private string _provider;
        public string Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(_displayName))
            {
                yield return new ValidationResult("名稱不得空白", new[] { "DisplayName" });
            }
            if (string.IsNullOrEmpty(_email))
            {
                yield return new ValidationResult("電子郵件不得空白", new[] { "Email" });
            }
        }
    }
}
