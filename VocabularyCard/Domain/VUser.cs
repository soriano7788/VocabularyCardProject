using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Domain
{
    public class VUser
    {
        // todo: 型別是否可改成 Guid?
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        // todo: email 是否抽出，另外當成通知類型??像是有 手機、line 之類的
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// 提供此 User 資料的來源，例如 google、ldap、facebook 等等
        /// 參考自 .net identity 的 AspNetUserLogins 的 LoginProvider
        /// </summary>
        private string _provider;
        public string Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }




    }
}
