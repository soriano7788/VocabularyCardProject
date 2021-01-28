using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VocabularyCard.AccountManager;
using VocabularyCard.AccountManager.Impl.Simple;
using VocabularyCard.AccountManager.Impl.Simple.Persistence;
using VocabularyCard.AccountManager.Impl.Simple.Persistence.EF;
using VocabularyCard.AccountManager.Impl.Simple.Persistence.EF.Mapping;
using VocabularyCard.Core.Interceptors;

namespace VocabularyCard.Web
{
    public class AccountManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // service
            builder.RegisterType<SimpleAccountManager>().As<IAccountManager>()
                .InstancePerLifetimeScope()
                .WithProperty("Salt", "123")
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor));

            // dao
            builder.RegisterType<EFSimpleUserDao>().As<ISimpleUserDao>().SingleInstance();

            // map
            builder.RegisterType<SimpleUserMap>().As<SimpleUserMap>().SingleInstance();
        }
    }
}
