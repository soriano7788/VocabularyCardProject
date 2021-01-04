using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.Impl.Simple.Domain;
using VocabularyCard.AccountManager.DTO;

namespace VocabularyCard.AccountManager.Impl.Simple.DTOConverter
{
    public class SimpleUserConverter
    {
        public SimpleUser ToDomainObject(RegisterInfo registerInfo)
        {
            SimpleUser simpleUser = new SimpleUser();
            simpleUser.UserId = string.Empty;
            simpleUser.Flag = 1;
            simpleUser.LoginId = registerInfo.LoginId;
            simpleUser.Password = registerInfo.Password;
            simpleUser.Email = string.Empty;

            return simpleUser;
        }
        public SimpleUser ToDomainObject(UserInfo userInfo)
        {
            SimpleUser simpleUser = new SimpleUser();
            simpleUser.UserId = userInfo.UserId;
            simpleUser.Flag = 1;
            simpleUser.LoginId = userInfo.LoginId;
            simpleUser.Email = userInfo.Email;

            return simpleUser;
        }
        public UserInfo ToDataTransferObject(SimpleUser simpleUser)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.UserId = simpleUser.UserId;
            userInfo.LoginId = simpleUser.LoginId;
            userInfo.DisplayName = simpleUser.DisplayName;
            userInfo.Email = simpleUser.Email;

            return userInfo;
        }

    }
}
