using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace VocabularyCard.Web.Filters
{
    public class Mvc5Authv1Attribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            throw new NotImplementedException();
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //throw new NotImplementedException();
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}