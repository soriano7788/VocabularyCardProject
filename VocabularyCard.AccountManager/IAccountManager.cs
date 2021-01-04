using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.DTO;

namespace VocabularyCard.AccountManager
{
    public interface IAccountManager
    {
        void Register(RegisterInfo registerInfo);
        UserInfo GetUserByUserId(string userId);
        UserInfo GetUserByLoginId(string loginId);
        bool CheckPassword(string loginId, string password);

        void UpdatePassword(string loginId, string oldPassword, string newPassword);
        void ResetPassword(string userId, string newPassword);
    }
}
