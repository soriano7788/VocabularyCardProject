using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;
using VocabularyCard.Persistence.EF.Mapping;

namespace VocabularyCard.Persistence.EF
{
    public class EFApiRefreshTokenDao : DbContextBase, IApiRefreshTokenDao
    {
        private ApiRefreshTokenMap _apiRefreshTokenMap;
        public EFApiRefreshTokenDao() : base() { }
        public EFApiRefreshTokenDao(ApiRefreshTokenMap apiRefreshTokenMap) : base()
        {
            _apiRefreshTokenMap = apiRefreshTokenMap;
        }
        public IDbSet<ApiRefreshToken> ApiRefreshTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(_apiRefreshTokenMap);
            base.OnModelCreating(modelBuilder);
        }

        public ApiRefreshToken Create(ApiRefreshToken refreshToken)
        {
            ApiRefreshToken result = ApiRefreshTokens.Add(refreshToken);
            SaveChanges();
            return result;
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteByToken(string token)
        {
            throw new NotImplementedException();
        }
        public ApiRefreshToken[] GetAll()
        {
            return ApiRefreshTokens.ToArray();
        }
        public ApiRefreshToken GetById(int id)
        {
            return ApiRefreshTokens.Find(id);
        }
        public ApiRefreshToken GetByToken(string token)
        {
            ApiRefreshToken refreshToken = ApiRefreshTokens.Where(t => t.Token == token).FirstOrDefault();
            return refreshToken;
        }
        public ApiRefreshToken[] GetAllByUserId(string usertId)
        {
            IQueryable<ApiRefreshToken> results = ApiRefreshTokens.Where(t => t.UserId == usertId);
            return results.ToArray();
        }
        public ApiRefreshToken[] GetAllValidByUserId(string usertId)
        {
            IQueryable<ApiRefreshToken> results = ApiRefreshTokens.Where(t => t.UserId == usertId && t.ExpiredDateTime > DateTime.UtcNow);
            return results.ToArray();
        }

        public ApiRefreshToken Update(ApiRefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
