using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager;
using VocabularyCard.Entities;
using VocabularyCard.Dtos;
using VocabularyCard.DtoConverters;
using VocabularyCard.Repositories;
using VocabularyCard.AccountManager.DTO;
using System.Security.Cryptography;
using VocabularyCard.Core;
using VocabularyCard.Core.Services;
using VocabularyCard.Util;
using System.Security.Claims;

namespace VocabularyCard.Services.Impl
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private IAccountManager _accountProvider;
        private IApiRefreshTokenRepository _refreshTokenRepository;
        private IApiAccessTokenRepository _accessTokenRepository;
        private JwtHelper _jwtHelper;

        private readonly ApiTokenConverter<ApiRefreshToken, ApiRefreshTokenDto> _refreshTokenConverter;
        private readonly ApiTokenConverter<ApiAccessToken, ApiAccessTokenDto> _accessTokenConverter;

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

        public AuthenticationService(
            IUnitOfWork unitOfWork,
            IAccountManager accountProvider, 
            IApiRefreshTokenRepository refreshTokenRepository, 
            IApiAccessTokenRepository accessTokenRepository, 
            JwtHelper jwtHelper) : base(unitOfWork)
        {
            _accountProvider = accountProvider;
            _refreshTokenRepository = refreshTokenRepository;
            _accessTokenRepository = accessTokenRepository;
            _jwtHelper = jwtHelper;
            _refreshTokenConverter = new ApiTokenConverter<ApiRefreshToken, ApiRefreshTokenDto>();
            _accessTokenConverter = new ApiTokenConverter<ApiAccessToken, ApiAccessTokenDto>();
        }


        public ApiAccessTokenDto CreateNewAccessToken(string refreshToken)
        {
            // todo: token 放 cache 晚點做
            ApiRefreshToken apiRefreshToken = _refreshTokenRepository.GetByToken(refreshToken);

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

            #endregion

            // 產生 access_token，參考一下 KM 的 oauth 看看(KM 還有一個 simple_nonce table，只為了記錄 nonce?)
            //string accessToken = GenerateNewToken(apiRefreshToken.UserId);
            string accessToken = GenerateJwtToken(apiRefreshToken.UserId);

            ApiAccessToken apiAccessToken = new ApiAccessToken
            {
                Token = accessToken,
                UserId = apiRefreshToken.UserId,
                CreatedDateTime = DateTime.UtcNow,
                ExpiredDateTime = DateTime.UtcNow.AddMinutes(_accessTokenLifeTime)
            };
            //ApiAccessToken result = _accessTokenRepository.Create(apiAccessToken);
            ApiAccessTokenDto dto = _accessTokenConverter.ToDataTransferObject(apiAccessToken);

            // todo: 現在的 dto 和 entity 沒啥差異，但是前端要的資訊是 幾秒內過期
            // 所以格式要轉換一下
            // 我覺得這段就交給 web 端處理，假如我在 dll 裡面就轉成秒數，假如以後要不同的時間格式，
            // dll 不就要修正? 當然可以把轉換時間格式這段 獨立出來 class 或 interface，然後透過參數設定要秒、分、時之類的，
            // 但我覺得就交給 web 端自行處理吧

            return dto;
        }

        public ApiAccessTokenDto GetValidAccessTokenByUserId(string userId)
        {
            IList<ApiAccessToken> accessTokens = _accessTokenRepository.GetAllValidByUserId(userId);
            if (accessTokens.Count() == 0)
            {
                return null;
            }
            return _accessTokenConverter.ToDataTransferObject(accessTokens.Last());
        }

        public void Register()
        {
            // 注意，帳號不可重複
            throw new NotImplementedException();
        }

        public AccessTokenValidatedResult ValidateAccessToken(string token)
        {
            JwtValidatedResult validatedResult = ValidateJwtToken(token);
            if (validatedResult.IsAuthenticated)
            {
                // 這樣不行，要解出 jwt token，取出 uid，此欄位存放了 userId

                return new AccessTokenValidatedResult
                {
                    IsAuthenticated = true,
                    UserInfo = new UserInfo { UserId = validatedResult.UserId }
                };
            }

            return new AccessTokenValidatedResult { IsAuthenticated = false };

            //ApiAccessToken accessToken = _accessTokenRepository.GetByToken(token);
            //// 此 AccessToken 不存在
            //if (accessToken == null)
            //{
            //    return new AccessTokenValidatedResult { IsAuthenticated = false };
            //}
            //// 此 AccessToken 已過期
            //if (accessToken.ExpiredDateTime < DateTime.UtcNow)
            //{
            //    return new AccessTokenValidatedResult { IsAuthenticated = false };
            //}
            //return new AccessTokenValidatedResult
            //{
            //    IsAuthenticated = true,
            //    UserInfo = new UserInfo { UserId = accessToken.UserId }
            //};
        }

        // 使用的情境??
        public RefreshTokenValidatedResult ValidateRefreshToken(string token)
        {
            ApiRefreshToken refreshToken = _refreshTokenRepository.GetByToken(token);

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

        public AuthenticationResult ValidateUser(string loginId, string password)
        {
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
            ApiRefreshTokenDto refreshTokenDto = GetValidRefreshTokenByUserId(userInfo.UserId);
            if (refreshTokenDto == null)
            {
                refreshTokenDto = CreateNewRefreshToken(userInfo.UserId);
            }

            #region 建立新的  access token
            // GenerateJwtToken(userInfo.UserId);
            //string accessTokenText = GenerateNewToken(userInfo.UserId);
            string accessTokenText = GenerateJwtToken(userInfo.UserId);

            ApiAccessToken apiAccessToken = new ApiAccessToken
            {
                Token = accessTokenText,
                UserId = userInfo.UserId,
                CreatedDateTime = DateTime.UtcNow,
                ExpiredDateTime = DateTime.UtcNow.AddMinutes(_accessTokenLifeTime)
            };
            //ApiAccessToken accessToken = _accessTokenRepository.Create(apiAccessToken);
            #endregion

            return new AuthenticationResult
            {
                IsAuthenticated = true,
                UserInfo = userInfo,
                Message = "OK",
                RefreshToken = refreshTokenDto.Token,
                RefreshTokenExpiredDateTime = refreshTokenDto.ExpiredDateTime,
                AccessToken = apiAccessToken.Token,
                AccessTokenExpiredDateTime = apiAccessToken.ExpiredDateTime
            };
        }



        #region 這部分 加解密 code 獨立出去比較好
        // sha256 為不可逆 雜湊
        private string EncodeToSHA256(string input, string salt)
        {
            input = input + salt;
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
        #endregion

        private ApiRefreshTokenDto CreateNewRefreshToken(string userId)
        {
            string refreshToken = GenerateNewToken(userId);

            ApiRefreshToken apiRefreshToken = new ApiRefreshToken
            {
                Token = refreshToken,
                UserId = userId,
                CreatedDateTime = DateTime.UtcNow,
                ExpiredDateTime = DateTime.UtcNow.AddMinutes(_refreshTokenLifeTime)
            };
            ApiRefreshToken result = _refreshTokenRepository.Create(apiRefreshToken);
            ApiRefreshTokenDto dto = _refreshTokenConverter.ToDataTransferObject(result);
            return dto;
        }
        private ApiRefreshTokenDto GetValidRefreshTokenByUserId(string userId)
        {
            IList<ApiRefreshToken> refreshTokens = _refreshTokenRepository.GetAllValidByUserId(userId);
            if (refreshTokens.Count() == 0)
            {
                return null;
            }
            return _refreshTokenConverter.ToDataTransferObject(refreshTokens.Last());
        }

        private string GenerateJwtToken(string userId)
        {
            return _jwtHelper.GetToken(userId, _accessTokenLifeTime);
        }
        private JwtValidatedResult ValidateJwtToken(string token)
        {
            ClaimsPrincipal principal;
            bool result = _jwtHelper.ValidateToken(token, out principal);

            if(result)
            {
                Claim uidClaim = principal.Claims.Where(claim => claim.Type == "uid").First();
                return new JwtValidatedResult
                {
                    IsAuthenticated = true,
                    UserId = uidClaim.Value
                };
            }

            return new JwtValidatedResult
            {
                IsAuthenticated = false,
                UserId = string.Empty
            };
        }


        private string GetNewGuid()
        {
            //return Guid.NewGuid().ToString().Replace("-", string.Empty);
            return Guid.NewGuid().ToString("N");
        }
        private string GenerateNewToken(string userId)
        {
            // 建立 access_token
            // 1. 先產生一個亂數 {guid}
            // 2. 取得 {userId}
            // 3. 取得加密用的 {salt}
            // 4. 原文組成為 {}|{}|{}

            // nonce: 每次都亂數產生的值，或許會用到?
            string originText = string.Format("{0}|{1}|{2}", GetNewGuid(), userId, _salt);
            string token = EncodeToSHA256(originText, _salt);
            return token;
        }
        private int CalculateExpiredSeconds(DateTime now, DateTime expiredDateTime)
        {
            if (now >= expiredDateTime)
            {
                return 0;
            }

            TimeSpan t = expiredDateTime - now;
            return (int)t.TotalSeconds;
        }

        class JwtValidatedResult
        {
            public bool IsAuthenticated { get; set; }
            public string UserId { get; set; }
        }
    }
}
