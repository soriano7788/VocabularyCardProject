using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VocabularyCard.Web.Models;
//using VocabularyCard.DTO;
//using VocabularyCard.Service;
using VocabularyCard.AccountManager;
using VocabularyCard.AccountManager.DTO;
using System.Web.Security;
using VocabularyCard.Services;
using VocabularyCard.Dtos;

namespace VocabularyCard.Web.Authentications
{
    public class VAuthenticator
    {
        private static IAuthenticationService _authenticationService
        {
            get
            {
                return new RepositoryFactory().GetAuthenticationService();
            }
        }
        private static IAccountManager _accountManager
        {
            get
            {
                return new AccountManagerRepositoryFactory().GetAccountManager();
            }
        }
        public static VAuthenticationResult ValidateUser(string loginId, string password)
        {
            UserInfo userInfo = _accountManager.GetUserByLoginId(loginId);
            if(userInfo == null)
            {
                return new VAuthenticationResult { Success = false, Message = "Invalid loginId or password" };
            }

            if(_accountManager.CheckPassword(loginId,password))
            {
                return new VAuthenticationResult { Success = true, Message = "Ok", UserInfo = userInfo };
            }
            else
            {
                return new VAuthenticationResult { Success = false, Message = "Invalid loginId or password" };
            }

        }

        public static AccessTokenValidatedResult ValidateAccessToken(string accessToken)
        {
            return _authenticationService.ValidateAccessToken(accessToken);
        }


        public static void CreateFormsAuthenticationCookie(UserInfo userInfo)
        {
            string guid = Guid.NewGuid().ToString();
            string userData = string.Format("{0}^{1}", userInfo.UserId, guid);

            DateTime now = DateTime.Now;
            DateTime expirarionDate = now.AddMinutes(30);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, 
                userInfo.LoginId, 
                now, 
                expirarionDate, 
                false, 
                userData);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authCookie.Expires = authTicket.Expiration;
            authCookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(authCookie);

            // 下面的目的與用途是???
            //string loginTimeoutCookieKey = WebUtility.GetLoginTimeoutCookieKey(authenticatedUser.LoginId);
            string loginTimeoutCookieKey = string.Format("loginTimeout_{0}", userInfo.UserId);
            var loginTimeoutCookie = new HttpCookie(loginTimeoutCookieKey)
            {
                HttpOnly = true,
                Value = DateTime.UtcNow.ToString(
                    "yyyy/MM/dd HH:mm:ss",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo
                    )
            };
            HttpContext.Current.Response.Cookies.Add(loginTimeoutCookie);

        }

    }
}
