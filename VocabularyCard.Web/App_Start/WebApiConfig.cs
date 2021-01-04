using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using VocabularyCard.Web.Filters;

namespace VocabularyCard.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
            );

            //強制輸出為 JSON
            //ref: http://blog.miniasp.com/post/2012/10/12/ASPNET-Web-API-Force-return-JSON-format-instead-of-XML-for-Google-Chrome-Firefox-Safari.aspx
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            // 指定輸出的日期 TimeZone 為 UTC
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

            config.Filters.Add(new WebApiAuthorizeAttribute());
            config.Filters.Add(new ApiResultAttribute());
            config.Filters.Add(new WebApiExceptionAttribute());

            #region 允許 CORS
            // todo: 第二個參數 headers 也是 *，是表示???
            // 是 headers 有哪些鍵值會造成不允許 CORS 嗎?
            // 不然 token-based 驗證 一般會把 token 放在 header，用來確認有存取權
            // 這要再查一下
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            #endregion
        }
    }
}
