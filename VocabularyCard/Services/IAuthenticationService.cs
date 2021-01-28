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
        void Register();
        AuthenticationResult ValidateUser(string loginId, string password);
        RefreshTokenValidatedResult ValidateRefreshToken(string token);
        AccessTokenValidatedResult ValidateAccessToken(string token);

        ApiAccessTokenDto GetValidAccessTokenByUserId(string userId);

        ApiAccessTokenDto CreateNewAccessToken(string refreshToken);
    }
}
