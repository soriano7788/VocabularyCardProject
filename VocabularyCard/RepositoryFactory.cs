using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Services;
using Autofac.Integration.Mvc;

namespace VocabularyCard
{
    public class RepositoryFactory
    {
        public IAuthenticationService GetAuthenticationService()
        {
            return AutofacDependencyResolver.Current.GetService(typeof(IAuthenticationService)) as IAuthenticationService;
        }
        public ICardSetService GetCardSetService()
        {
            return AutofacDependencyResolver.Current.GetService(typeof(ICardSetService)) as ICardSetService;
        }
        public ICardService GetCardService()
        {
            return AutofacDependencyResolver.Current.GetService(typeof(ICardService)) as ICardService;
        }
    }
}
