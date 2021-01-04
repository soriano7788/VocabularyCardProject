using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using VocabularyCard.Persistence;
using VocabularyCard.Persistence.EF;

namespace VocabularyCard.Web
{
    public class DaoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //string connectionString = BuildConnectionString();
            // WithParameter 是手動建構式注入
            //builder.RegisterType<EFCardDao>().As<ICardDao>().WithParameter("nameOrConnectionString", connectionString).SingleInstance();
            //builder.RegisterType<EFCardSetDao>().As<ICardSetDao>().WithParameter("nameOrConnectionString", connectionString).SingleInstance();

            builder.RegisterType<EFCardDao>().As<ICardDao>().SingleInstance();
            builder.RegisterType<EFCardSetDao>().As<ICardSetDao>().SingleInstance();
            builder.RegisterType<EFApiRefreshTokenDao>().As<IApiRefreshTokenDao>().SingleInstance();
            builder.RegisterType<EFApiAccessTokenDao>().As<IApiAccessTokenDao>().SingleInstance();
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
