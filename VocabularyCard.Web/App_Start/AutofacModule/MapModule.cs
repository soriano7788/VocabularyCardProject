using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VocabularyCard.Persistence.EF.Mapping;

namespace VocabularyCard.Web
{
    public class MapModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CardMap>()/*.As<CardMap>()*/.SingleInstance();
            builder.RegisterType<CardSetMap>()/*.As<CardSetMap>()*/.SingleInstance();
            builder.RegisterType<ApiRefreshTokenMap>().SingleInstance();
            builder.RegisterType<ApiAccessTokenMap>().SingleInstance();
        }
    }
}
