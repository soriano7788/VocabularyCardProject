using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using VocabularyCard.AccountManager.DTO;

namespace VocabularyCard.Web.Models
{
    public class VIdentity : IIdentity
    {
        private readonly string _name;
        private readonly UserInfo _userInfo;

        public VIdentity(string name, UserInfo userInfo)
        {
            _name = name;
            _userInfo = userInfo;
        }

        #region VIdentity 成員
        public UserInfo UserInfo
        {
            get { return _userInfo; }
        }
        #endregion

        #region IIdentity 成員
        public string Name
        {
            get { return _name; }
        }
        public string AuthenticationType
        {
            get { return "VTokenAuthentication"; }
        }
        public bool IsAuthenticated
        {
            get { return true; }
        }
        #endregion
    }
}
