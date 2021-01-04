using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.DTO;

namespace VocabularyCard.DTO
{
    public abstract class TokenValidatedResult
    {
        public bool IsAuthenticated { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
