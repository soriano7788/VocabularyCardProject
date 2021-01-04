using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VocabularyCard.Validation;

namespace VocabularyCard.Web.Models
{
    public class TestUserInfo
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _displayName;
        [UserName(InvalidName = "fuck")]
        [Remote("UserName", "Validations")]  // 這個是讓前端用 ajax 驗證資料是否 OK 用的
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        private string _email;
        [Email]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}