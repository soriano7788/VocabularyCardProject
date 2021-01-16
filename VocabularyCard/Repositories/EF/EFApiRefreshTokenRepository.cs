using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.EF;
using VocabularyCard.Core.Repositories;
using VocabularyCard.Entities;

namespace VocabularyCard.Repositories.EF
{
    public class EFApiRefreshTokenRepository : EFBaseApiTokenRepository<ApiRefreshToken>, IApiRefreshTokenRepository
    {
        private IDbSet<ApiRefreshToken> _refreshTokens;

        public EFApiRefreshTokenRepository(IDbContext context) : base(context)
        {
            _refreshTokens = context.Set<ApiRefreshToken>();
        }
    }
}
