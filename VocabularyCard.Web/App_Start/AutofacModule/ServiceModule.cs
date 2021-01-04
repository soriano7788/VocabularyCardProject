using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VocabularyCard.Service;
using VocabularyCard.Service.Impl;

namespace VocabularyCard.Web
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CardService>().As<ICardService>().SingleInstance().WithProperty("VocabularyLengthLimit", 100);
            builder.RegisterType<CardSetService>().As<ICardSetService>().SingleInstance();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance()
                .WithProperty("RefreshTokenLifeTime", 3600)  // 單位秒
                .WithProperty("AccessTokenLifeTime", 1800);  // 單位秒
        }
    }
}
