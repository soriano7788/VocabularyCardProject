using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace VocabularyCard.Web.Filters
{
    public class Mvc5Authv2Attribute : ActionFilterAttribute, IAuthenticationFilter, IOverrideFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            Global.Log.Debug("OnAuthentication");
            // 測試用，角色不該寫死，應該由當下 context 取得
            filterContext.Principal = new GenericPrincipal(filterContext.HttpContext.User.Identity, new string[] { "Admin" });
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            Global.Log.Debug("OnAuthenticationChallenge");
            IPrincipal user = filterContext.HttpContext.User;
            if ((user == null) || (!user.Identity.IsAuthenticated && !user.IsInRole("Admin")))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public Type FiltersToOverride
        {
            get { return typeof(IAuthenticationFilter); }
        }
    }
}