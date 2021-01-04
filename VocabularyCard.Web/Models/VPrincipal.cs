using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace VocabularyCard.Web.Models
{
    public class VPrincipal : IPrincipal
    {
        private readonly IIdentity _identity;

        public VPrincipal(IIdentity identity)
        {
            _identity = identity;
        }

        #region IPrincipal 成員
        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
