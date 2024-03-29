using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VocabularyCard.AccountManager;
using VocabularyCard.Dtos;
using VocabularyCard.Services;
using VocabularyCard.Web.Extensions;
using VocabularyCard.Web.Models;

namespace VocabularyCard.Web.Api
{
    public class AccountController : ApiController
    {
        private IAccountManager _accountManager;
        private IAuthenticationService _authenticationService;
        public AccountController(IAccountManager accountManager, IAuthenticationService authenticationService)
        {
            _accountManager = accountManager;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        public void Register()
        {

            // return refresh_token、access_token、access_token_expired_in?
        }

        [AllowAnonymous]
        [HttpPost]
        public AuthenticationResult SignIn(VLoginViewModel viewModel)
        {
            AuthenticationResult a = _authenticationService.ValidateUser(viewModel.LoginId, viewModel.Password);
            return a;
            // return refresh_token、access_token、access_token_expired_in
        }

        public void SignOut()
        { }

        [AllowAnonymous]
        [HttpPost]
        /// <summary>
        /// 取得新的 access_token，需先傳入 refresh_token
        /// </summary>
        public ApiAccessTokenDto GetAccessToken([FromBody] string refreshToken)
        {
            Global.Log.Error("GetAccessToken refreshToken: " + refreshToken);
            ApiAccessTokenDto  accessTokenDto = _authenticationService.CreateNewAccessToken(refreshToken);
            return accessTokenDto;

            //return new AccessToken
            //{
            //    Token = accessTokenDto.Token,
            //    ExpiresIn = CalculateExpiredSeconds(accessTokenDto.CreatedDateTime, accessTokenDto.ExpiredDateTime)
            //};
        }

        private int CalculateExpiredSeconds(DateTime createdDateTime, DateTime expiredDateTime)
        {
            if (createdDateTime >= expiredDateTime)
            {
                return 0;
            }

            TimeSpan t = expiredDateTime - createdDateTime;
            return (int)t.TotalSeconds;
        }

    }
}
