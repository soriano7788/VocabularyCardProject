using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager;
using VocabularyCard.AccountManager.Impl.Simple.Domain;
using VocabularyCard.AccountManager.Impl.Simple.Persistence.EF.Mapping;
using System.Configuration;

namespace VocabularyCard.AccountManager.Impl.Simple.Persistence.EF
{
    public class EFSimpleUserDao : DbContext, ISimpleUserDao
    {
        private SimpleUserMap _simpleUserMap;
        public EFSimpleUserDao(SimpleUserMap simpleUserMap) : base()
        {
            _simpleUserMap = simpleUserMap;

            string connectionString = BuildConnectionString();
            this.Database.Connection.ConnectionString = connectionString;
        }

        public IDbSet<SimpleUser> SimpleUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(_simpleUserMap);

            base.OnModelCreating(modelBuilder);
        }

        public SimpleUser GetByUserId(string userId)
        {
            return SimpleUsers.Find(userId);
        }
        public SimpleUser GetByLoginId(string loginId)
        {
            return SimpleUsers.Where(u => u.LoginId == loginId).First();
        }
        public void Create(SimpleUser user)
        {
            SimpleUsers.Add(user);
            SaveChanges();
        }
        public void Update(SimpleUser user)
        {
            throw new NotImplementedException();
        }
        public void Delete(string userId)
        {
            throw new NotImplementedException();
        }

        private string BuildConnectionString()
        {

            StringBuilder sb = new StringBuilder();
            string dbSource = ConfigurationManager.AppSettings["DbSource"];
            string dbName = ConfigurationManager.AppSettings["DbName"];
            string userId = ConfigurationManager.AppSettings["UserId"];
            string pwd = ConfigurationManager.AppSettings["Password"];
            sb.AppendFormat("Data Source={0}; Initial Catalog={1}; Integrated Security=False;User Id={2};Password={3};MultipleActiveResultSets=True",
                dbSource,
                dbName,
                userId,
                pwd);

            return sb.ToString();
        }
    }
}
