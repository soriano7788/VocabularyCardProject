using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VocabularyCard.Web.Models
{
    public class ApiResult
    {
        public string statusCode { get; set; }

        public string msg { get; set; }

        public object data { get; set; }
    }
}
