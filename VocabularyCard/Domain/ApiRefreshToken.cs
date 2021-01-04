using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Domain
{
    public class ApiRefreshToken
    {
        private int _vuid;
        public int Vuid
        {
            get { return _vuid; }
            set { _vuid = value; }
        }

        private string _token;
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private DateTime _createdDateTime;
        public DateTime CreatedDateTime
        {
            get { return _createdDateTime; }
            set { _createdDateTime = value; }
        }

        private DateTime _expiredDateTime;
        public DateTime ExpiredDateTime
        {
            get { return _expiredDateTime; }
            set { _expiredDateTime = value; }
        }

    }
}
