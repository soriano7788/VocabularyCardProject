using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VocabularyCard.Web.Filters
{
    public class CardSetResultFilterAttribute : ActionFilterAttribute, IResultFilter
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Global.Log.Debug("OnResultExecuting");
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Global.Log.Debug("OnResultExecuted");
        }
    }
}
