using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.DTO;
using VocabularyCard.AccountManager.Impl.Simple.Domain;
using VocabularyCard.AccountManager.Impl.Simple.Persistence;
using VocabularyCard.AccountManager.Impl.Simple.DTOConverter;
using VocabularyCard.Core.Utils;

namespace VocabularyCard.AccountManager.Impl.Simple
{
    public class SimpleAccountManager : IAccountManager
    {
        private string _salt = string.Empty;
        public string Salt 
        {
            set
            {
                _salt = value;
            }
        }

        private ISimpleUserDao _simpleUserDao;

        public SimpleAccountManager(ISimpleUserDao simpleUserDao)
        {
            _simpleUserDao = simpleUserDao;
        }

        public void Register(RegisterInfo registerInfo)
        {
            // todo: 檢查 帳號是否重複，密碼搭配 salt 加密

            string newUserId = Guid.NewGuid().ToString();
            var converter = new SimpleUserConverter();
            SimpleUser simpleUser = converter.ToDomainObject(registerInfo);
            
            simpleUser.UserId = newUserId;
            simpleUser.Flag = 1;
            simpleUser.Password = HashHelper.ComputeSHA256Hash(string.Concat(simpleUser.Password, _salt));

            _simpleUserDao.Create(simpleUser);
        }
        public UserInfo GetUserByUserId(string userId)
        {
            SimpleUser simpleUser = _simpleUserDao.GetByUserId(userId);
            if (simpleUser == null)
            {
                return null;
            }

            UserInfo userInfo = new SimpleUserConverter().ToDataTransferObject(simpleUser);
            return userInfo;
        }
        public UserInfo GetUserByLoginId(string loginId)
        {
            SimpleUser simpleUser = _simpleUserDao.GetByLoginId(loginId);
            if(simpleUser == null)
            {
                return null;
            }

            UserInfo userInfo = new SimpleUserConverter().ToDataTransferObject(simpleUser);
            return userInfo;
        }
        public bool CheckPassword(string loginId, string password)
        {
            SimpleUser simpleUser = _simpleUserDao.GetByLoginId(loginId);
            if (simpleUser == null)
            {
                return false;
            }

            return (simpleUser.Password == HashHelper.ComputeSHA256Hash(string.Concat(password, _salt)));
        }
        public void UpdatePassword(string loginId, string oldPassword, string newPassword) 
        {
            throw new NotImplementedException();
        }
        public void ResetPassword(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
