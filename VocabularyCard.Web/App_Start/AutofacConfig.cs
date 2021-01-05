using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Extensions.Configuration;
using VocabularyCard.Domain;
using VocabularyCard.Persistence;
using VocabularyCard.Persistence.EF;
using VocabularyCard.Persistence.EF.Mapping;
using VocabularyCard.Service;
using VocabularyCard.Service.Impl;
using VocabularyCard.Test.Repository;
using VocabularyCard.Test.Repository.EF;
using VocabularyCard.Test.Repository.EF.Configuration;
using VocabularyCard.Test.Service;
using VocabularyCard.Test.Service.Impl;
using VocabularyCard.Repositories.EF.Mapping;
using VocabularyCard.Repositories.EF;
using VocabularyCard.Repositories;
using VocabularyCard.Services;
using VocabularyCard.Services.Impl;
using VocabularyCard.Core.EF;
using VocabularyCard.Core.Repositories;
using VocabularyCard.Core.Services;

namespace VocabularyCard.Web
{
    public class AutofacConfig
    {
        public static void Register()
        {
            #region autofac config by json

            //var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            //config.AddJsonFile("autofac.json");
            //var module = new ConfigurationModule(config.Build());
            //ContainerBuilder builderZZZ = new ContainerBuilder();
            //builderZZZ.RegisterModule(module);

            //return;

            #endregion



            // 這邊可以動態載入設定檔，所以我想下面那團 DI 是否可再獨立出去
            // 還有 controller 的 建構式參數 DI，是否能夠自己手動設定?
            // 現在只有註冊各 class，其餘都自動偵測去注入，總感覺不踏實...

            ContainerBuilder builder = new ContainerBuilder();

            /// import json 的話，到這邊結束 ///


            #region (X)Assembly 抓錯，應該抓 VocabularyCard.dll 的
            // 這邊直接 Assembly.GetExecutingAssembly() 會抓到這個 MVC 專案的 dll
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //   .Where(t => t.Name.EndsWith("Map"))
            //   .AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //   .Where(t => t.Name.EndsWith("Dao"))
            //   .AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //       .Where(t => t.Name.EndsWith("Service"))
            //       .AsImplementedInterfaces();
            #endregion

            #region 嘗試抓 VocabularyCard 就可以了
            //var assembly = Assembly.Load("VocabularyCard");
            //// Map 沒 interface，所以就不用 AsImplementedInterfaces() 了，加了反而會出錯
            //builder.RegisterAssemblyTypes(assembly)
            //   .Where(t => t.Name.EndsWith("Map"))
            //   .SingleInstance();

            //builder.RegisterAssemblyTypes(assembly)
            //   .Where(t => t.Name.EndsWith("Dao"))
            //   .AsImplementedInterfaces().SingleInstance();

            //builder.RegisterAssemblyTypes(assembly)
            //       .Where(t => t.Name.EndsWith("Service"))
            //       .AsImplementedInterfaces().SingleInstance();
            #endregion

            // controller 就在本 mvc 專案，所以這樣寫就可以了
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());



            #region 把 DI 註冊再獨立出去 ***Module class 檔
            // 參考說明
            // https://dotblogs.com.tw/Jamis/2015/12/23/163505

            // service
            builder.RegisterModule(new ServiceModule());
            //dao
            builder.RegisterModule(new DaoModule());
            // map
            builder.RegisterModule(new MapModule());
            // account manager
            builder.RegisterModule(new AccountManagerModule());

            #endregion


            #region 新的設計方式註冊

            // orm mapping
            builder.RegisterType<Repositories.EF.Mapping.CardMap>().As<Core.EF.IEntityTypeConfiguration>().SingleInstance();
            builder.RegisterType<Repositories.EF.Mapping.CardSetMap>().As<Core.EF.IEntityTypeConfiguration>().SingleInstance();

            // EF dbContext
            builder.RegisterType(typeof(Core.EF.BaseDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            // unitOfWork
            builder.RegisterType(typeof(Core.EF.EFUnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();

            // repository
            builder.RegisterGeneric(typeof(Core.EF.EFBaseRepository<>)).As(typeof(Core.Repositories.IRepository<>)).InstancePerDependency();
            builder.RegisterType(typeof(Repositories.EF.EFCardSetRepository)).As(typeof(Repositories.ICardSetRepository)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(Repositories.EF.EFCardRepository)).As(typeof(Repositories.ICardRepository)).InstancePerLifetimeScope();



            // service
            builder.RegisterType(typeof(Core.Services.BaseService)).As(typeof(IService)).InstancePerDependency();

            builder.RegisterType<Services.Impl.CardService>()
                .As<Services.ICardService>()
                //.InstancePerLifetimeScope();
                .SingleInstance();
            builder.RegisterType<Services.Impl.CardSetService>()
                .As<Services.ICardSetService>()
                //.InstancePerLifetimeScope();
                .SingleInstance();

            #endregion



            #region 測試 unit of work

            // 設定 map 檔，是否能像這樣註冊????為了讓 BaseDbContext 每冊都注入同一批 Map 設定
            // (不過這樣不確定會不會有其他問題，例如 隨著 BaseDbContext 生命週期結束，Map List 也被釋放掉等等)
            //builder.RegisterType({ cardSetMap}, { cardMap}).As(typeof(IMap[]))

            //builder.RegisterType<CardSetConfiguration>().As<IEntityTypeConfiguration>().SingleInstance();
            //builder.RegisterType<CardConfiguration>().As<IEntityTypeConfiguration>().SingleInstance();

            //builder.RegisterType(typeof(BaseDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();


            //builder.RegisterType(typeof(EFUnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();


            //builder.RegisterAssemblyTypes(ReferencedAssemblies.Repositories).
            //    Where(_ => _.Name.EndsWith("Repository")).
            //    AsImplementedInterfaces().
            //    InstancePerLifetimeScope();

            //builder.RegisterType(typeof(EFCardSetRepository)).As(typeof(ICardSetRepository)).InstancePerLifetimeScope();
            //builder.RegisterType(typeof(EFCardRepository)).As(typeof(ICardRepository)).InstancePerLifetimeScope();

            //builder.RegisterGeneric(typeof(EFBaseRepository<>)).As(typeof(IRepository<>)).InstancePerDependency();

            //// 功能就是被繼承，還有留一個 public unitOfWork 屬性，給子類別使用
            //builder.RegisterType(typeof(BaseService)).As(typeof(BaseService)).InstancePerDependency();

            // service
            builder.RegisterType<VocabularyCard.Test.Service.Impl.CardSetService>()
                .As<VocabularyCard.Test.Service.ICardSetService>()
                //.InstancePerLifetimeScope();
                .SingleInstance();







            #endregion









            // 建立容器
            IContainer container = builder.Build();

            // 解析容器內的型別
            AutofacWebApiDependencyResolver resolverApi = new AutofacWebApiDependencyResolver(container);
            AutofacDependencyResolver resolver = new AutofacDependencyResolver(container);
            // 建立相依解析器
            GlobalConfiguration.Configuration.DependencyResolver = resolverApi;
            DependencyResolver.SetResolver(resolver);

        }
    }
}
