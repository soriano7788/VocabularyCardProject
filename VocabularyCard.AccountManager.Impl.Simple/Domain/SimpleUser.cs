using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.AccountManager.Impl.Simple.Domain
{
    public class SimpleUser
    {
        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private int _flad;
        public int Flag
        {
            get { return _flad; }
            set { _flad = value; }
        }

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

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

    }
}
