using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VocabularyCard.AccountManager.DTO;

namespace VocabularyCard.Web.Models
{
    public class VAuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}