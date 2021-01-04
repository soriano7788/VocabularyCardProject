using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VocabularyCard.Web.Models;

namespace VocabularyCard.Web.Extensions
{
    public class AjaxJsonResult : ActionResult
    {
        private static bool _success = false;
        private static object _data = null;

        public AjaxJsonResult(bool success, object data)
        {
            _success = success;
            _data = data;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            var result = new AjaxResult
            {
                Success = _success,
                data = _data
            };
            HttpContext.Current.Response.Write(SerializeToJson(result));
        }

        private static string SerializeToJson(object obj)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }
    }
}