using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VocabularyCard.Web.Filters
{
    public class CardSetActionFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Global.Log.Debug("OnActionExecuting");
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Global.Log.Debug("OnActionExecuted");
        }
    }
}
