using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace VocabularyCard.Persistence.EF
{
    public class DbContextBase : DbContext
    {
        private Guid _uniqueKey = Guid.NewGuid();
        public Guid UniqueKey
        {
            get
            {
                return _uniqueKey;
            }
        }

        public DbContextBase()
        {
            // base 內的參數是 web.config 裡設定的 connectionString 的 name
            // 所以我應該再建一個 DbContextBase，裡面先做好 connectionString 設定?
            // 不然每個地方都要注一次很麻煩
            // 之前 KM 每個 DAO 實作 是有一個 GetSession method，裡面可能有做類似的事??

            // todo: 讓 DbContextBase 的無參數建構式，裡面設定好 connectionString 就好，是否可行??
            // todo: 先從某個 utility，取出連線字串，在這裡設定
            // todo: 此 utility，需從設定檔拿到 db 位置、加密的帳號密碼(要解密)，再組出連線線字串

            // Data Source=.\SQLExpress; Initial Catalog=vocabulary; Integrated Security=False;User Id=sa;Password=ua5tjonGgOFI;MultipleActiveResultSets=True
            string connectionString = BuildConnectionString();
            this.Database.Connection.ConnectionString = connectionString;
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
