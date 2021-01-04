using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VocabularyCard.AccountManager;
using VocabularyCard.AccountManager.Impl.Simple;
using VocabularyCard.AccountManager.Impl.Simple.Persistence;
using VocabularyCard.AccountManager.Impl.Simple.Persistence.EF;
using VocabularyCard.AccountManager.Impl.Simple.Persistence.EF.Mapping;

namespace VocabularyCard.Web
{
    public class AccountManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // service
            builder.RegisterType<SimpleAccountManager>().As<IAccountManager>().SingleInstance().WithProperty("Salt", "123");

            // dao
            builder.RegisterType<EFSimpleUserDao>().As<ISimpleUserDao>().SingleInstance();

            // map
            builder.RegisterType<SimpleUserMap>().As<SimpleUserMap>().SingleInstance();
        }
    }
}