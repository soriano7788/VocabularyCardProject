using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace VocabularyCard.Core.EF
{
    public class BaseDbContext : DbContext
    {
        private IEnumerable<IEntityTypeConfiguration> _mapConfigurations;

        public BaseDbContext(IEnumerable<IEntityTypeConfiguration> mapConfigurations) : base(BuildConnectionString())
        {
            _mapConfigurations = mapConfigurations;
            Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (dynamic config in _mapConfigurations)
            {
                // todo: 需注意，未來擴充功能後，mapConfig 不一定都會在同一個 assembly(元件) 裡
                // 這應該是解法
                // https://dotnetfalcon.com/untitled/

                // 轉 dynamic 參考自這個
                // https://stackoom.com/question/12kIu/AutoFac-DbContext%E9%97%AE%E9%A2%98-%E5%9C%A8%E6%A8%A1%E5%9E%8B%E5%88%9B%E5%BB%BA%E6%97%B6%E6%97%A0%E6%B3%95%E4%BD%BF%E7%94%A8

                // 結果轉 dynamic 竟然就解掉了....但是這樣好像不太安全，要能確保 config 必定是 EntityTypeConfiguration 型別
                // 假如有其他不相關的 class 亂實作 IEntityTypeConfiguration，然後又去 autofac 註冊，
                // 這邊就會爆掉
                // 我要不要故意測試一下???
                // 試一下好了
                modelBuilder.Configurations.Add(config);
            }
        }

        private static string BuildConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            string dbSource = ConfigurationManager.AppSettings["DbSource"];
            string dbName = ConfigurationManager.AppSettings["DbName"];
            string userId = ConfigurationManager.AppSettings["UserId"];
            string pwd = ConfigurationManager.AppSettings["Password"];

            // todo: 假如換成 oracle、mariaDB 之類的，連線字串也是長這樣嗎?
            sb.AppendFormat("Data Source={0}; Initial Catalog={1}; Integrated Security=False;User Id={2};Password={3};MultipleActiveResultSets=True",
                dbSource,
                dbName,
                userId,
                pwd);

            return sb.ToString();
        }
    }
}
