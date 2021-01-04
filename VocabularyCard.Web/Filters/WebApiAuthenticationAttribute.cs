using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
//using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace VocabularyCard.Web.Filters
{
    // 無法用在 web api controller
    public class WebApiAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        // todo: 還是說 api controller 不能用 IAuthenticationFilter???
        // 疑似是這樣??? 查到說的只有三種 Filter: AuthorizationFilterAttribute、ActionFilterAttribute 、ExceptionFilterAttribute
        // 沒有 IAuthenticationFilter
        // 且 namespace 是 System.Web.Http.Filters???!!!
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            Global.Log.Error("WebApi OnAuthentication");

            NameValueCollection headers = filterContext.RequestContext.HttpContext.Request.Headers;
            string token = headers.Get("v-auth");

            // todo: 檢查是否有 v-auth，再來檢查 token 是否有效 (spa 用的 token)

            Global.Log.Error("token: " + token);
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            Global.Log.Error("WebApi OnAuthenticationChallenge");

            //var user = filterContext.HttpContext.User;
            //if (user == null || !user.Identity.IsAuthenticated)
            //{
            //    filterContext.Result = new HttpUnauthorizedResult();
            //}
        }
    }
}
