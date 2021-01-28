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
using VocabularyCard.Repositories.EF;
using VocabularyCard.Repositories.EF.Mapping;
using VocabularyCard.Repositories;
using VocabularyCard.Services;
using VocabularyCard.Services.Impl;
using VocabularyCard.CacheProvider;
using VocabularyCard.CacheProvider.Impl.InMemoryCache;
using VocabularyCard.Core;
using VocabularyCard.Core.EF;
using VocabularyCard.Core.Repositories;
using VocabularyCard.Core.Services;
using VocabularyCard.Core.Interceptors;
using Autofac.Extras.DynamicProxy;

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

            //// service
            //builder.RegisterModule(new ServiceModule());
            ////dao
            //builder.RegisterModule(new DaoModule());
            //// map
            //builder.RegisterModule(new MapModule());

            // account manager
            builder.RegisterModule(new AccountManagerModule());

            #endregion


            // 註冊類型
            // SingleInstance: 就是 singleton 啦
            // InstancePerRequest: 在同一個 request scope 下，會共用的 instance，生命週期也只維持到 request 結束前吧?
            // InstancePerLifetimeScope:
            // InstancePerDependency: 每次要用到都 new 一個
            // 說明: https://toyo0103.github.io/2018/07/12/%E3%80%90Autofac%E3%80%91%E7%94%9F%E5%91%BD%E9%80%B1%E6%9C%9F/

            // Autofac 預設使用的生命週期即為 InstancePerDependency


            #region 新的設計方式註冊

            // 看了一下下面這些設定，像是 RegisterType()、As()、InstancePerLifetimeScope()，
            // 回傳的都是 IRegistrationBuilder interface，所以可以一直 call 同 interface 的 method，
            // 這會像 decorator pattern 嗎? 不過跟 decorator pattern 的大部分範例結構又不太像....

            // orm mapping
            builder.RegisterType<CardMap>().As<IEntityTypeConfiguration>().SingleInstance();
            builder.RegisterType<CardSetMap>().As<IEntityTypeConfiguration>().SingleInstance();
            builder.RegisterType<CardInterpretationMap>().As<IEntityTypeConfiguration>().SingleInstance();

            // EF dbContext
            builder.RegisterType(typeof(BaseDbContext)).As(typeof(IDbContext)).InstancePerLifetimeScope();

            // unitOfWork
            builder.RegisterType(typeof(EFUnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();

            // repository
            builder.RegisterGeneric(typeof(EFBaseRepository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterType(typeof(EFCardSetRepository)).As(typeof(ICardSetRepository)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(EFCardRepository)).As(typeof(ICardRepository)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(EFCardInterpretationRepository)).As(typeof(ICardInterpretationRepository)).InstancePerLifetimeScope();

            // cacheProvider
            builder.RegisterType<MemoryCacheProvider>()
                .As<ICacheProvider>()
                .SingleInstance();

            // service
            builder.RegisterType(typeof(BaseService)).As(typeof(IService)).InstancePerDependency();

            builder.RegisterType<CardService>()
                .As<ICardService>()
                .InstancePerLifetimeScope()
                .WithProperty("NameCacheMinutes", 20)
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor));

            builder.RegisterType<CardSetService>()
                .As<ICardSetService>()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor));

            builder.RegisterType<CardInterpretationService>()
                .As<ICardInterpretationService>()
                .InstancePerLifetimeScope().EnableInterfaceInterceptors();

            // 註冊 Transaction Interceptor
            builder.RegisterType(typeof(TransactionInterceptor))
                .WithProperty("TransactionMethodsPrefix", new string[] { "Create", "Update", "Delete" })
                .WithProperty("TransactionMethods", new string[] { "ValidateUser" })
                .WithProperty("IgnoreTransactionMethods", new string[] { "CreateCard" });

            // authentication 相關
            builder.RegisterType<ApiRefreshTokenMap>().As<IEntityTypeConfiguration>().SingleInstance();
            builder.RegisterType<ApiAccessTokenMap>().As<IEntityTypeConfiguration>().SingleInstance();

            builder.RegisterGeneric(typeof(EFBaseApiTokenRepository<>)).As(typeof(IBaseApiTokenRepository<>)).InstancePerDependency();
            builder.RegisterType(typeof(EFApiRefreshTokenRepository)).As(typeof(IApiRefreshTokenRepository)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(EFApiAccessTokenRepository)).As(typeof(IApiAccessTokenRepository)).InstancePerLifetimeScope();

            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>()
                .InstancePerLifetimeScope()
                .WithProperty("RefreshTokenLifeTime", 3600)  // 單位秒
                .WithProperty("AccessTokenLifeTime", 1800)  // 單位秒
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor));

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
