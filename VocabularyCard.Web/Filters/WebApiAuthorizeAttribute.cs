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

            AuthenticationHeaderValue authorization = actionContext.Request.Headers.Authorization;
            if(authorization != null && authorization.Scheme == "Bearer")
            {
                string token = authorization.Parameter;
                var result = VAuthenticator.ValidateAccessToken(token);
                if(result.IsAuthenticated)
                {
                    SetCurrentUser(result.UserInfo);
                }
                else
                {
                    ThrowUnauthorizedException();
                }
            }
            else
            {
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
