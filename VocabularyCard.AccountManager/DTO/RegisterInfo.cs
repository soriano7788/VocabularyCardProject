using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.AccountManager.DTO
{
    public class RegisterInfo
    {
        private string _loginId;
        public string LoginId
        {
            get { return _loginId; }
            set { _loginId = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }
    }
}
