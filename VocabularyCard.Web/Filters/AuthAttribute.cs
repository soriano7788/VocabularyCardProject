using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;
using VocabularyCard.Web.Extensions;

namespace VocabularyCard.Web.Filters
{
    public class AuthAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {

            //IPrincipal user = filterContext.HttpContext.User;
            //if (user == null || !user.Identity.IsAuthenticated)
            //{
            //    filterContext.Result = new AjaxJsonResult(false, "Unauthorized");
            //}
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            // 這段適合在這邊? 
            // 還是 Global.asax.cs 的 protected void Application_AuthenticateRequest(object sender, EventArgs e) 呢?
            // 在 global 那邊，每個 request 都檢查的話，login request 也會被擋掉

            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[cookieName];

            if (null == authCookie)
            {
                filterContext.Result = new AjaxJsonResult(false, "Unauthorized");
                return;
            }

            FormsAuthenticationTicket authTicket = null;
            authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (null == authTicket)
            {
                filterContext.Result = new AjaxJsonResult(false, "Unauthorized");
                return;
            }
            if (authTicket.UserData == string.Empty)
            {
                filterContext.Result = new AjaxJsonResult(false, "Unauthorized");
                return;
            }

            // 既然有持續在連線，就更新過期時間吧，這裡是不是可以直接用之前的 VAuthenticator.CreateFormsAuthenticationCookie??
            // 不行，因為沒 userInfo
            FormsAuthenticationTicket newAuthTicket = new FormsAuthenticationTicket(
                1,
                authTicket.Name,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                authTicket.UserData);

            string encryptedTicket = FormsAuthentication.Encrypt(newAuthTicket);

            HttpCookie newAuthCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            newAuthCookie.Expires = newAuthTicket.Expiration;
            newAuthCookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Set(newAuthCookie);



            #region 先留著
            //IPrincipal user = filterContext.HttpContext.User;
            //if (user == null || !user.Identity.IsAuthenticated)
            //{
            //    filterContext.Result = new AjaxJsonResult(false, "Unauthorized");
            //}
            #endregion

        }
    }
}