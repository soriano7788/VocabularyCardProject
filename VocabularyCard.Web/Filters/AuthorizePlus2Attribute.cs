using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VocabularyCard.Web.Filters
{
    public class AuthorizePlus2Attribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // 從 request 取得 cookie，key 為 mvcAuth 的 cookie，value 為 token
            HttpCookie hc = filterContext.HttpContext.Request.Cookies["mvcAuth"];
            string token = string.Empty;

            if (hc != null)
            {
                token = hc.Value;
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                // 401 未授權
                base.HandleUnauthorizedRequest(filterContext);
            }

            DateTime loginTime = Convert.ToDateTime(filterContext.HttpContext.Application[token]);
            if (loginTime > DateTime.UtcNow)
            {
                // 驗證通過
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}