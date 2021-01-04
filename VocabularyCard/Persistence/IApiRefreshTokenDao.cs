using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Persistence
{
    public interface IApiRefreshTokenDao
    {
        ApiRefreshToken[] GetAll();
        ApiRefreshToken[] GetAllByUserId(string usertId);
        ApiRefreshToken[] GetAllValidByUserId(string usertId);

        ApiRefreshToken GetById(int id);
        ApiRefreshToken GetByToken(string token);
        ApiRefreshToken Create(ApiRefreshToken refreshToken);
        ApiRefreshToken Update(ApiRefreshToken refreshToken);
        void DeleteById(int id);
        void DeleteByToken(string token);
    }
}
