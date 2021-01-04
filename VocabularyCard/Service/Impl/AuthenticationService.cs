using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;
using VocabularyCard.DTO;
using VocabularyCard.DTOConverter;
using VocabularyCard.Persistence;
using VocabularyCard.AccountManager.DTO;
using VocabularyCard.AccountManager;
using VocabularyCard.Util;
using System.Security.Cryptography;

namespace VocabularyCard.Service.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        private Dictionary<string, ApiRefreshTokenInfo> _refreshTokenCache 
        { 
            get
            {
                if(_refreshTokenCache == null)
                {
                    _refreshTokenCache = new Dictionary<string, ApiRefreshTokenInfo>();
                }
                return _refreshTokenCache;
            }
            set
            {
                _refreshTokenCache = value;
            }
        }

        private Dictionary<string, ApiAccessTokenInfo> _accessTokenCache
        {
            get
            {
                if (_accessTokenCache == null)
                {
                    _accessTokenCache = new Dictionary<string, ApiAccessTokenInfo>();
                }
                return _accessTokenCache;
            }
            set
            {
                _accessTokenCache = value;
            }
        }

        private IAccountManager _accountProvider;
        private IApiRefreshTokenDao _apiRefreshTokenDao;
        private IApiAccessTokenDao _apiAccessTokenDao;

        private int _refreshTokenLifeTime;
        public int RefreshTokenLifeTime
        {
            set { _refreshTokenLifeTime = value; }
        }

        private int _accessTokenLifeTime;
        public int AccessTokenLifeTime
        {
            set { _accessTokenLifeTime = value; }
        }

        private string _salt;
        public string Salt
        {
            set { _salt = value; }
        }

        public AuthenticationService(IAccountManager accountProvider, IApiRefreshTokenDao apiRefreshTokenDao, IApiAccessTokenDao apiAccessTokenDao)
        {
            _accountProvider = accountProvider;
            _apiRefreshTokenDao = apiRefreshTokenDao;
            _apiAccessTokenDao = apiAccessTokenDao;
        }

        public void Regester()
        {
            // 注意，帳號不可重複
        }
        public AuthenticationResult ValidateUser(string loginId, string password)
        {
            // 只負責告訴呼叫端驗證成功或失敗
            // 或是包成 AuthenticationResult 物件，裡面有驗證結果，access_token、refresh_token 之類的?

            //bool isAuthenticated = false;
            UserInfo userInfo = _accountProvider.GetUserByLoginId(loginId);
            if (userInfo == null)
            {
                return new AuthenticationResult { IsAuthenticated = false, Message = "loginId or password error." };
            }

            if (!_accountProvider.CheckPassword(loginId, password))
            {
                return new AuthenticationResult { IsAuthenticated = false, Message = "loginId or password error." };
            }

            // 先檢查有沒有現有有效的 refreshToken，沒有的話就建一個新的
            ApiRefreshTokenInfo refreshTokenInfo = GetValidRefreshTokenByUserId(userInfo.UserId);
            if(refreshTokenInfo == null)
            {
                refreshTokenInfo = CreateNewRefreshToken(userInfo.UserId);
            }

            // accessToken 因為時效短，就不嘗試找現有有效的，直接建新的
            ApiAccessTokenInfo accessTokenInfo = CreateNewAccessToken(userInfo.UserId, refreshTokenInfo.Token);

            // refresh_token 剩幾秒過期
            TimeSpan r = refreshTokenInfo.ExpiredDateTime - DateTime.UtcNow;
            double rSec = r.TotalSeconds;

            // access_token 剩幾秒過期
            TimeSpan a = accessTokenInfo.ExpiredDateTime - DateTime.UtcNow;
            double aSec = a.TotalSeconds;


            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("DateTimeUtcNow: {0}{1}", DateTime.UtcNow, Environment.NewLine);
            //sb.AppendFormat("refreshTokenInfo.ExpiredDateTime: {0}{1}", refreshTokenInfo.ExpiredDateTime, Environment.NewLine);
            //sb.AppendFormat("accessTokenInfo.ExpiredDateTime: {0}{1}", accessTokenInfo.ExpiredDateTime, Environment.NewLine);
            //sb.AppendFormat("rSec: {0}{1}", rSec, Environment.NewLine);
            //sb.AppendFormat("aSec: {0}{1}", aSec, Environment.NewLine);
            //LogUtility.ErrorLog(sb.ToString());


            return new AuthenticationResult
            {
                IsAuthenticated = true,
                UserInfo = userInfo,
                Message = "OK",
                RefreshToken = refreshTokenInfo.Token,
                RefreshTokenExpiresIn = (int)rSec,
                AccessToken = accessTokenInfo.Token,
                AccessTokenExpiresIn = (int)aSec
            };
        }
        public ApiRefreshTokenInfo CreateNewRefreshToken(string userId)
        {
            string refreshToken = GenerateNewToken(userId);

            ApiRefreshToken apiRefreshToken = new ApiRefreshToken
            {
                Token = refreshToken,
                UserId = userId,
                CreatedDateTime = DateTime.UtcNow,
                ExpiredDateTime = DateTime.UtcNow.AddSeconds(_refreshTokenLifeTime)
            };
            ApiRefreshToken result = _apiRefreshTokenDao.Create(apiRefreshToken);
            return new ApiRefreshTokenConverter().ToDataTransferObject(result);
        }

        public ApiAccessTokenInfo CreateNewAccessToken(string userId, string refreshToken)
        {
            UserInfo userInfo = _accountProvider.GetUserByUserId(userId);

            // todo: token 放 cache 晚點做
            ApiRefreshToken apiRefreshToken = _apiRefreshTokenDao.GetByToken(refreshToken);

            #region exception檢查
            // 是否再嘗試細分 exception class 類型?
            if (apiRefreshToken == null)
            {
                throw new ArgumentException("refreshToken not exist");
            }

            if (apiRefreshToken.ExpiredDateTime < DateTime.UtcNow)
            {
                throw new ArgumentException("refreshToken expired");
            }

            if (apiRefreshToken.UserId != userId)
            {
                throw new ArgumentException(string.Format("refreshToken not belong to userId: {0}", userId));
            }
            #endregion

            #region 產生 access_token，參考一下 KM 的 oauth 看看(原來KM還有一個 simple_nonce table 阿，之前都沒仔細看)
            // 建立 access_token
            // 1. 先產生一個亂數 {guid}
            // 2. 取得 {userId}
            // 3. 取得加密用的 {salt}
            // 4. 原文組成為 {}|{}|{}

            // nonce: 每次都亂數產生的值，或許會用到?

            string accessToken = GenerateNewToken(userId);

            #endregion

            ApiAccessToken apiAccessToken = new ApiAccessToken
            {
                Token = accessToken,
                UserId = userId,
                CreatedDateTime = DateTime.UtcNow,
                ExpiredDateTime = DateTime.UtcNow.AddSeconds(_accessTokenLifeTime)
            };
            ApiAccessToken result = _apiAccessTokenDao.Create(apiAccessToken);
            return new ApiAccessTokenConverter().ToDataTransferObject(result);
        }

        public ApiRefreshTokenInfo GetValidRefreshTokenByUserId(string userId)
        {
            ApiRefreshToken[] refreshTokens =_apiRefreshTokenDao.GetAllValidByUserId(userId);
            if(refreshTokens.Length == 0)
            {
                return null;
            }
            return new ApiRefreshTokenConverter().ToDataTransferObject(refreshTokens.Last());
        }
        public ApiAccessTokenInfo GetValidAccessTokenByUserId(string userId)
        {
            ApiAccessToken[] accessTokens = _apiAccessTokenDao.GetAllValidByUserId(userId);
            if (accessTokens.Length == 0)
            {
                return null;
            }
            return new ApiAccessTokenConverter().ToDataTransferObject(accessTokens.Last());
        }


        public void GetAccessToken(ApiRefreshTokenInfo refreshTokenInfo)
        {
            if(refreshTokenInfo.ExpiredDateTime >= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("RefreshToken was expired");
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// 驗證傳入的 RefreshToken 是否有效。
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public RefreshTokenValidatedResult ValidateRefreshToken(string token)
        {
            ApiRefreshToken refreshToken = _apiRefreshTokenDao.GetByToken(token);

            // 此 RefreshToken 不存在
            if (refreshToken == null)
            {
                return new RefreshTokenValidatedResult { IsAuthenticated = false };
            }
            // 此 RefreshToken 已過期
            if (refreshToken.ExpiredDateTime < DateTime.UtcNow)
            {
                return new RefreshTokenValidatedResult { IsAuthenticated = false };
            }
            return new RefreshTokenValidatedResult 
            { 
                IsAuthenticated = true, 
                UserInfo = new UserInfo { UserId = refreshToken.UserId } 
            };
        }

        /// <summary>
        /// 驗證傳入的 AccessToken 是否有效。
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public AccessTokenValidatedResult ValidateAccessToken(string token)
        {
            ApiAccessToken accessToken = _apiAccessTokenDao.GetByToken(token);
            // 此 AccessToken 不存在
            if (accessToken == null)
            {
                return new AccessTokenValidatedResult { IsAuthenticated = false };
            }
            // 此 AccessToken 已過期
            if (accessToken.ExpiredDateTime < DateTime.UtcNow)
            {
                return new AccessTokenValidatedResult { IsAuthenticated = false };
            }
            return new AccessTokenValidatedResult
            {
                IsAuthenticated = true,
                UserInfo = new UserInfo { UserId = accessToken.UserId }
            };
        }


        // sha256 為不可逆 雜湊
        private string EncodeToSHA256(string input)
        {
            input = input + _salt;
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(input);//將字串轉為Byte[]
            byte[] crypto = sha256.ComputeHash(source);//進行SHA256加密
            string output = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
            return output;
        }

        private string ConvertStringToBase64(string input)
        {
            byte[] bts = Encoding.UTF8.GetBytes(input);
            string resultEncode = Convert.ToBase64String(bts);
            return resultEncode;
        }
        private string ConvertBase64ToString(string input)
        {
            byte[] bts = Convert.FromBase64String(input);
            string resultDecode = Convert.ToBase64String(bts);
            return resultDecode;
        }
        private string GetNewGuid()
        {
            //return Guid.NewGuid().ToString().Replace("-", string.Empty);
            return Guid.NewGuid().ToString("N");
        }

        private string GenerateNewToken(string userId)
        {
            string originText = string.Format("{0}|{1}|{2}", GetNewGuid(), userId, _salt);
            string token = EncodeToSHA256(originText);
            return token;
        }

    }
}
