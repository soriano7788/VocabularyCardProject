using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Repositories;
using VocabularyCard.Entities;

namespace VocabularyCard.Repositories
{
    public interface IBaseApiTokenRepository<TToken> : IRepository<TToken> where TToken : BaseApiToken
    {
        void DeleteById(int id);
        void DeleteByToken(string token);
        TToken GetById(int id);
        TToken GetByToken(string token);
        IList<TToken> GetAllByUserId(string usertId);
        IList<TToken> GetAllValidByUserId(string usertId);
    }
}
