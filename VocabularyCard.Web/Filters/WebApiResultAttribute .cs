using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using VocabularyCard.Web.Models;

namespace VocabularyCard.Web.Filters
{
    public class ApiResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // 避免 exception 發生時，執行到這個?
            if (actionExecutedContext.Exception != null)
            {
                return;
            }

            // ignored type is one of...
            // 1. ApiResultAttribute 
            // 2. Base types of ApiResultAttribute
            Func<IgnoreFilterAttribute, bool> ignoreCheck = (r) =>
            {
                return r.FilterType.IsAssignableFrom(typeof(ApiResultAttribute));
            };


            // 不想被包裝的話，需要下面這區塊
            var ignoredActions = actionExecutedContext
                .ActionContext
                .ActionDescriptor
                .GetCustomAttributes<IgnoreFilterAttribute>()
                .Any(ignoreCheck);
                //.FirstOrDefault();

            var ignoredControllers = actionExecutedContext
                .ActionContext
                .ControllerContext
                .ControllerDescriptor
                .GetCustomAttributes<IgnoreFilterAttribute>()
                .Any(ignoreCheck);
            //.FirstOrDefault();

            if (ignoredActions || ignoredControllers)
            {
                return;
            }

            //if (ignoredActions != null || ignoredControllers != null)
            //{
            //    return;
            //}

            var objectContent = actionExecutedContext.Response.Content as ObjectContent;

            var data = objectContent?.Value;

            // 假如有特殊的 case，要指定 msg，那要從哪邊切入?
            var result = new ApiResult
            {
                statusCode = ApiStatusCode.SUCCESS,
                msg = string.Empty,
                data = data
            };

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result);
        }
    }
}
