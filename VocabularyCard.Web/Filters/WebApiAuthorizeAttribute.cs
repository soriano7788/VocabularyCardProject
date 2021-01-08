using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using VocabularyCard.Exceptions;
using VocabularyCard.Web.Authentications;
using VocabularyCard.Web.Models;
using System.Threading;
using System.Security.Principal;
using System.Diagnostics.Contracts;
using VocabularyCard.AccountManager.DTO;
using VocabularyCard.Dtos;

namespace VocabularyCard.Web.Filters
{
    public class WebApiAuthorizeAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                return;
            }

            if (SkipAuthorization(actionContext))
            {
                return;
            }

            HttpRequestHeaders headers = actionContext.Request.Headers;
            string AUTH_LEY = "authorization";

            if (headers.Contains(AUTH_LEY))
            {
                // 注意: header name 不存在的話，會直接 噴 exception
                var authValues = headers.GetValues(AUTH_LEY);
                string value = authValues.First();

                string accessToken = string.Empty;
                if(value.StartsWith("bearer "))
                {
                    accessToken = value.Split(' ')[1];
                }

                Global.Log.Error("extract authorization value: " + accessToken);

                // 驗證 token
                AccessTokenValidatedResult result = VAuthenticator.ValidateAccessToken(accessToken);
                if (!result.IsAuthenticated)
                {
                    Global.Log.Error("ValidateAccessToken fail");
                    // 驗證不通過，注意 ! CreateResponse 是 System.Net.Http 命名空間的擴充方法
                    //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                    ThrowUnauthorizedException();
                }
                else
                {
                    Global.Log.Error("ValidateAccessToken success");
                    SetCurrentUser(result.UserInfo);
                }
            }
            else
            {
                Global.Log.Error("headers not contain authorization");
                // 驗證不通過，注意 ! CreateResponse 是 System.Net.Http 命名空間的擴充方法
                //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                // 未授權改這樣是否比較好??? 可以讓 ApiExceptionAttribute 再去進一步處理
                ThrowUnauthorizedException();
            }
        }
        private static void SetCurrentUser(UserInfo userInfo)
        {
            IIdentity identity = new VIdentity(userInfo.UserId, userInfo);
            IPrincipal principal = new VPrincipal(identity);

            // 先不使用 Thread 的 Principal
            //Thread.CurrentPrincipal = principal;

            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }


        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            // 參考
            // https://stackoverflow.com/questions/19671411/custom-api-authorize-ignoring-allowanonymous/23195075

            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
        private static void ThrowUnauthorizedException()
        {
            //var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Oops!!!" };
            throw new VUnauthorizedException();
        }
    }
}
