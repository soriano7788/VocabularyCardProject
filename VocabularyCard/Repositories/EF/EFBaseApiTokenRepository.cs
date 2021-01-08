using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.EF;
using VocabularyCard.Entities;

namespace VocabularyCard.Repositories.EF
{
    public class EFBaseApiTokenRepository<TToken> : EFBaseRepository<TToken>, IBaseApiTokenRepository<TToken> where TToken: BaseApiToken
    {
        private IDbSet<TToken> _baseTokens;

        public EFBaseApiTokenRepository(DbContext context) : base(context)
        {
            _baseTokens = context.Set<TToken>();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteByToken(string token)
        {
            throw new NotImplementedException();
        }

        public IList<TToken> GetAllByUserId(string usertId)
        {
            return _baseTokens.Where(t => t.UserId == usertId).ToList();
        }

        public IList<TToken> GetAllValidByUserId(string usertId)
        {
            return _baseTokens.Where(t => t.UserId == usertId && t.ExpiredDateTime > DateTime.UtcNow).ToList();
        }

        public TToken GetById(int id)
        {
            return _baseTokens.Find(id);
        }

        public TToken GetByToken(string token)
        {
            return _baseTokens.Where(t => t.Token == token).FirstOrDefault();
        }
    }
}
