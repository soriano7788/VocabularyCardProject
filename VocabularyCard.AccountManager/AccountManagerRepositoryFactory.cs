using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.AccountManager
{
    public class AccountManagerRepositoryFactory
    {
        public IAccountManager GetAccountManager()
        {
            return AutofacDependencyResolver.Current.GetService(typeof(IAccountManager)) as IAccountManager;
        }
    }
}
