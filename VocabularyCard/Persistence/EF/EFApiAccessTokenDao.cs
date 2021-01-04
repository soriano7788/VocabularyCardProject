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
    public class EFApiAccessTokenDao : DbContextBase, IApiAccessTokenDao
    {
        private ApiAccessTokenMap _apiAccessTokenMap;
        public EFApiAccessTokenDao() : base() { }
        public EFApiAccessTokenDao(ApiAccessTokenMap apiAccessTokenMap) : base()
        {
            _apiAccessTokenMap = apiAccessTokenMap;
        }
        public IDbSet<ApiAccessToken> ApiAccessTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(_apiAccessTokenMap);
            base.OnModelCreating(modelBuilder);
        }

        public ApiAccessToken Create(ApiAccessToken accessToken)
        {
            ApiAccessToken result = ApiAccessTokens.Add(accessToken);
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
        public ApiAccessToken[] GetAll()
        {
            return ApiAccessTokens.ToArray();
        }
        public ApiAccessToken[] GetAllByUserId(string usertId)
        {
            IQueryable<ApiAccessToken> results = ApiAccessTokens.Where(t => t.UserId == usertId);
            return results.ToArray();
        }
        public ApiAccessToken[] GetAllValidByUserId(string usertId)
        {
            IQueryable<ApiAccessToken> results = ApiAccessTokens.Where(t => t.UserId == usertId && t.ExpiredDateTime > DateTime.UtcNow);
            return results.ToArray();
        }

        public ApiAccessToken GetById(int id)
        {
            return ApiAccessTokens.Find(id);
        }
        public ApiAccessToken GetByToken(string token)
        {
            ApiAccessToken accessToken = ApiAccessTokens.Where(t => t.Token == token).FirstOrDefault();
            return accessToken;
        }

        public ApiAccessToken Update(ApiAccessToken accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
