using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Entities;

namespace VocabularyCard.Entities
{
    public class ApiAccessToken : BaseEntity
    {
        public int Vuid { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ExpiredDateTime { get; set; }
    }
}
