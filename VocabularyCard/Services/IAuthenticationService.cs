using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Services;
using VocabularyCard.Dtos;

namespace VocabularyCard.Services
{
    public interface IAuthenticationService : IService
    {
        void Regester();
        AuthenticationResult ValidateUser(string loginId, string password);
        RefreshTokenValidatedResult ValidateRefreshToken(string token);
        AccessTokenValidatedResult ValidateAccessToken(string token);

        ApiRefreshTokenDto GetValidRefreshTokenByUserId(string userId);
        ApiAccessTokenDto GetValidAccessTokenByUserId(string userId);

        ApiRefreshTokenDto CreateNewRefreshToken(string userId);

        ApiAccessTokenDto CreateNewAccessToken(string userId, string refreshToken);
    }
}
