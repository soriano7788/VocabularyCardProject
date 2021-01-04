using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Persistence
{
    public interface IApiAccessTokenDao
    {
        ApiAccessToken[] GetAll();
        ApiAccessToken[] GetAllByUserId(string usertId);
        ApiAccessToken[] GetAllValidByUserId(string usertId);

        ApiAccessToken GetById(int id);
        ApiAccessToken GetByToken(string token);
        ApiAccessToken Create(ApiAccessToken accessToken);
        ApiAccessToken Update(ApiAccessToken accessToken);
        void DeleteById(int id);
        void DeleteByToken(string token);
    }
}
