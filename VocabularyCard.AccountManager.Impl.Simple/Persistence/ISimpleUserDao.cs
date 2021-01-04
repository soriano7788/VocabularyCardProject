using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.Impl.Simple.Domain;

namespace VocabularyCard.AccountManager.Impl.Simple.Persistence
{
    public interface ISimpleUserDao
    {
        SimpleUser GetByUserId(string userId);
        SimpleUser GetByLoginId(string loginId);
        void Create(SimpleUser user);
        void Update(SimpleUser user);
        void Delete(string userId);
    }
}
