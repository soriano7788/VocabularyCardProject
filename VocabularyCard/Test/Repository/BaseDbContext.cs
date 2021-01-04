using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Test.Repository.EF.Configuration;
using VocabularyCard.Domain;
using VocabularyCard.Util;

namespace VocabularyCard.Test.Repository
{
    public class BaseDbContext : DbContext
    {
        private IEnumerable<IEntityTypeConfiguration> _mapConfigurations;

        public BaseDbContext(IEnumerable<IEntityTypeConfiguration> mapConfigurations) : base(BuildConnectionString())
        {
            //_mapConfigurations = mapConfigurations;
            LogUtility.ErrorLog("(mapConfigurations == null): " + (mapConfigurations == null));
            if(mapConfigurations != null)
            {
                LogUtility.ErrorLog("mapConfigurations.Count: " + mapConfigurations.Count());
            }

            _mapConfigurations = mapConfigurations;

            LogUtility.ErrorLog("finish");
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            LogUtility.ErrorLog("AAA");

            foreach (IEntityTypeConfiguration config in _mapConfigurations)
            {
                LogUtility.ErrorLog("add config: " + config.GetType());

                // todo: 需注意， mapConfig 之後不一定都會在同一個 assembly(元件) 裡
                // 這應該是解法
                // https://dotnetfalcon.com/untitled/


                // 轉 dynamic 參考自這個
                // https://stackoom.com/question/12kIu/AutoFac-DbContext%E9%97%AE%E9%A2%98-%E5%9C%A8%E6%A8%A1%E5%9E%8B%E5%88%9B%E5%BB%BA%E6%97%B6%E6%97%A0%E6%B3%95%E4%BD%BF%E7%94%A8

                // 結果轉 dynamic 竟然就解掉了....但是這樣好像不太安全，要能確保 config 必定是 EntityTypeConfiguration 型別
                // 假如有其他不相關的 class 亂實作 IEntityTypeConfiguration，然後又去 autofac 註冊，
                // 這邊就會爆掉
                // 我要不要故意測試一下???
                // 試一下好了
                modelBuilder.Configurations.Add((dynamic)config);
                //modelBuilder.Add(config);

                // 也有從 assembly 來增加的方式，好像也是可以考慮的方法??
                // 把設定全部集中在一個 assembly
                //modelBuilder.Configurations.AddFromAssembly();

                // todo: 再來是 EFUnitOfWork 裡面的 public IRepository<T> Repository<T>() where T : class
                // 這個也應該要讓 autofac 來處理比較好吧


            }

            LogUtility.ErrorLog("BBB");

            //modelBuilder.Configurations.Add(new CardSetConfiguration());
            //modelBuilder.Configurations.Add(new CardConfiguration());
        }

        private static string BuildConnectionString()
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
