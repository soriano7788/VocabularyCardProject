using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.DTO;

namespace VocabularyCard.Service
{
    public interface IAuthenticationService
    {
        void Regester();
        AuthenticationResult ValidateUser(string loginId, string password);
        RefreshTokenValidatedResult ValidateRefreshToken(string token);
        AccessTokenValidatedResult ValidateAccessToken(string token);

        ApiRefreshTokenInfo GetValidRefreshTokenByUserId(string userId);
        ApiAccessTokenInfo GetValidAccessTokenByUserId(string userId);

        ApiRefreshTokenInfo CreateNewRefreshToken(string userId);

        ApiAccessTokenInfo CreateNewAccessToken(string userId, string refreshToken);


    }
}
