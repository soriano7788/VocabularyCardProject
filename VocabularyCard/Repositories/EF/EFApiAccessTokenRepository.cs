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
    public class EFApiAccessTokenRepository : EFBaseApiTokenRepository<ApiAccessToken>, IApiAccessTokenRepository
    {
        private IDbSet<ApiAccessToken> _accessTokens;

        public EFApiAccessTokenRepository(IDbContext context) : base(context)
        {
            _accessTokens = context.Set<ApiAccessToken>();
        }
    }
}
