using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using VocabularyCard.Web.Extensions;
using VocabularyCard.Web.Models;
using VocabularyCard.AccountManager.DTO;
using System.Threading;
using System.Security.Principal;

namespace VocabularyCard.Web.Util
{
    public class WebUtility
    {
        private static RepositoryFactory _repository;
        public static RepositoryFactory Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new RepositoryFactory();
                }
                return _repository;
            }
        }

        public static string SafeHtml(string html)
        {
            string filterString = string.IsNullOrEmpty(html) ? string.Empty : html.Trim();
            if (string.IsNullOrEmpty(filterString))
            {
                return null;
            }
            // 只有 br、p、em、span、style 這些 html 標籤會保留，
            // 其餘的 html 標籤會被拿掉
            // e.g. <script>zzz</script> 會剩下 zzz
            Regex regex = new Regex(@"<(?!br|\/?p|style|\/?span)[^>]*>");
            filterString = regex.Replace(filterString, "");

            return filterString;
        }
        public static UserInfo GetCurrentUser()
        {
            // 暫時先不判斷每一階是否為 null

            // 先忽略 Thread 的
            #region extract userInfo from thread
            //if (Thread.CurrentPrincipal != null)
            //{
            //    return RetrieveUserInfoFromPrincipal(Thread.CurrentPrincipal);
            //}
            #endregion

            #region extract userInfo from HttpContext

            if (HttpContext.Current != null)
            {
                return RetrieveUserInfoFromPrincipal(HttpContext.Current.User);
            }
            #endregion

            return null;
        }
        public static UserInfo RetrieveUserInfoFromPrincipal(IPrincipal principal)
        {
            // 這邊突然覺得有泛型的話就很方便了，可以直接 Thread.CurrentPrincipal<VPrincipal> 這樣
            VPrincipal vPrincipal = principal as VPrincipal;
            VIdentity identity = vPrincipal.Identity as VIdentity;
            return identity.UserInfo;
        }

        public static string GetUserData()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity id = HttpContext.Current.User.Identity as FormsIdentity;
                FormsAuthenticationTicket ticket = id.Ticket;
                var userInfo = id.Ticket.UserData;
                return userInfo;
            }
            return string.Empty;
        }

        //public static T TestNewFunctionality<T>(T t) : where new T()
        //{
        //    return new T();
        //}

        public static AjaxJsonResult ReturnAjaxResult(bool success, object data)
        {
            return new AjaxJsonResult(success, data);
        }
    }
}
