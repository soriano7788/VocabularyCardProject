using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Dtos
{
    public class BaseApiTokenDto
    {
        public int Vuid { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ExpiredDateTime { get; set; }
    }
}
