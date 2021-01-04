using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using VocabularyCard.Exceptions;
using VocabularyCard.Web.Models;

namespace VocabularyCard.Web.Filters
{
    public class WebApiExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Global.Log.Error(actionExecutedContext.Exception);

            if(actionExecutedContext.Exception is VUnauthorizedException)
            {
                // 注意 ! CreateResponse 是 System.Net.Http 命名空間的擴充方法
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                var result = new ApiResult
                {
                    statusCode = ApiStatusCode.UNEXPECTED_ERROR,
                    msg = actionExecutedContext.Exception.Message
                };

                // todo: 根據 exception 類型，設定不同的 code
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result);
            }
        }
    }
}
