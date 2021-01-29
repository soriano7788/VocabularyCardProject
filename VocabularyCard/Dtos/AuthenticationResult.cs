using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.DTO;

namespace VocabularyCard.Dtos
{
    // todo: 是否抽出一層 AuthBaseResult，只保留 IsAuthenticated、UserId
    // 其餘的 refreshToken、accessToken result 另行繼承，自行加 property?
    public class AuthenticationResult
    {
        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { _isAuthenticated = value; }
        }

        private UserInfo _userInfo;
        public UserInfo UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _refreshToken;
        public string RefreshToken
        {
            get { return _refreshToken; }
            set { _refreshToken = value; }
        }

        private string _accessToken;
        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        private DateTime _refreshTokenExpiredDateTime;
        public DateTime RefreshTokenExpiredDateTime
        {
            get { return _refreshTokenExpiredDateTime; }
            set { _refreshTokenExpiredDateTime = value; }
        }

        private DateTime _accessTokenExpiredDateTime;
        public DateTime AccessTokenExpiredDateTime
        {
            get { return _accessTokenExpiredDateTime; }
            set { _accessTokenExpiredDateTime = value; }
        }
    }
}
