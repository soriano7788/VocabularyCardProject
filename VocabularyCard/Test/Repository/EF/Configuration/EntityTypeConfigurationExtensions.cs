using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Test.Repository.EF.Configuration
{
    public static class EntityTypeConfigurationExtensions
    {
        private static MethodInfo GetAddMethod(Type inputType) =>
            typeof(ConfigurationRegistrar)
                .GetMethods()
                .Single(m => m.Name == nameof(ConfigurationRegistrar.Add) &&
                 m.GetParameters().Count() == 1 &&
                 m.GetParameters()[0].ParameterType.IsGenericType &&
                 m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == inputType
                 );

        private static readonly MethodInfo addEntityTypeConfigMethod = GetAddMethod(typeof(EntityTypeConfiguration<>));
        private static readonly MethodInfo addComplexTypeConfigMethod = GetAddMethod(typeof(ComplexTypeConfiguration<>));

        public static void Add(this ConfigurationRegistrar configurationRegistrar, IEntityTypeConfiguration entityTypeConfiguration) =>
           Add(configurationRegistrar, addEntityTypeConfigMethod, entityTypeConfiguration);


        public static void Add(this ConfigurationRegistrar configurationRegistrar, IComplexTypeConfiguration complexTypeConfiguration) =>
           Add(configurationRegistrar, addComplexTypeConfigMethod, complexTypeConfiguration);

        private static void Add(ConfigurationRegistrar configurationRegistrar, MethodInfo addMethod, object configuration)
        {
            var type = configuration.GetType();
            var genericAdd = addComplexTypeConfigMethod.MakeGenericMethod(type);
            genericAdd.Invoke(configurationRegistrar, new[] { configuration });
        }
    }
}
