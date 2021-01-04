using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VocabularyCard.Web.Models;

namespace VocabularyCard.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // 比對順序由上而下依序，只要比對到一個符合的，就不會繼續下面的
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 範例，網址不包含 controller name 的話，就按照照預設值，選擇 HomeController
            //routes.MapRoute(
            //    name: "Test",  // 名稱必須是唯一
            //    url: "{action}/{id}",  // 移除掉 controller
            //    defaults: new
            //    {
            //        controller = "Home",  // 網址樣式移除了 {Controller} 就必須吃預設值
            //        action = "Index",
            //        id = UrlParameter.Optional
            //    },
            //    //constraints: new { action = "About" },  // 限制只有當 {action} 的值是 About 時，才 map 這條規則
            //    //constraints: new { action = "(About|Contact)" },  // 可用正規表示式
            //    namespaces: new[] { "WebApplication.Controller"}  // 用 namespace 來限制，只有 namespace 相同的才會比對成功
            //);
            // 還有一種實作 IRouteConstraint 的方式，用來驗證參數是否符合規則

            //routes.MapRoute(
            //    name: "Register",
            //    url: "Account/VRegister/{viewModel}",
            //    defaults: new { controller = "Account", action = "VRegister", viewModel = typeof(VRegisterViewModel) }
            //);

            // 建立時預設的路由
            //routes.MapRoute(
            //    // Route 的名稱，此名稱必須是唯一，否則會發生例外
            //    name: "Default",
            //    // 定義 URL Routing 的樣式，其中被 {} 包住的變數除了 controller 和 action 都可以依據自己的需要自訂義。
            //    url: "{controller}/{action}/{id}",
            //    // 當傳入的網址比對不到的時候就會依據 defaults 定義的值替代，
            //    // 在這裡要特別提到其中 id 的設定 UrlParameter.Optional(選擇性參數)，
            //    // 這個意思是即使網址中沒有 id 也會預設通過
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            // catch all requests route
            routes.MapRoute(
                name: "Everything",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index" }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "",
            //    defaults: new { controller = "Home", action = "Index" }
            //);

        }
    }
}
