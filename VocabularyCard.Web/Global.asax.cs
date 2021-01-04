using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
using System.Web.Helpers;
using System.Text;

namespace VocabularyCard.Web
{
    public class Global : System.Web.HttpApplication
    {
        public static readonly Logger Log = LogManager.GetLogger("WEB");
        private static IContainer Container { get; set; }
        protected void Application_Start()
        {
            try
            {
                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                AutofacConfig.Register();

                //// 將 antiForgeryToken 的 cookie name 改成 MVC_BOOK5，避免預設的 name
                //AntiForgeryConfig.CookieName = "MVC_BOOK5";
                //// 自行客製加上的驗證
                //AntiForgeryConfig.AdditionalDataProvider = new AntiForgeryExtension();

                // 調整 ViewEngine 順序
                ViewEngines.Engines.Clear();
                ViewEngines.Engines.Add(new RazorViewEngine());
                //ViewEngines.Engines.Add(new WebFormViewEngine());
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                throw;
            }
        }


        /// <summary>
        /// 所有 request 進來，不論來自網站，或是其他程式等等呼叫 web api，
        /// 都會先跑到這邊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

            #region Request Cookies
            //StringBuilder sbCookies = new StringBuilder();
            //sbCookies.AppendFormat("--------Request Cookies>>>--------\r\n");

            ////output：/Default3.aspx?id=1019&uu=77
            //sbCookies.AppendFormat("  Request.RawUrl: \"{0}\"\r\n", Context.Request.RawUrl);

            ////output：/Default3.aspx
            //sbCookies.AppendFormat("  Request.Path: \"{0}\"\r\n", Context.Request.Path);

            //sbCookies.AppendFormat("  Cookies.Count: {0}\r\n", Context.Request.Cookies.Count);

            //// 一般要從網站來，這邊才有 cookies
            //foreach (string cookieKey in Context.Request.Cookies.Keys)
            //{
            //    sbCookies.AppendFormat("    Cookie[\"{0}\"]\r\n", cookieKey);
            //    var cookie = Context.Request.Cookies[cookieKey];
            //    if (cookie != null)
            //    {
            //        sbCookies.AppendFormat("      Secure: {0}\r\n", cookie.Secure);
            //        sbCookies.AppendFormat("      Domain: {0}\r\n", cookie.Domain);
            //        sbCookies.AppendFormat("      Expires: {0}\r\n", cookie.Expires);
            //        sbCookies.AppendFormat("      HttpOnly: {0}\r\n", cookie.HttpOnly);
            //        sbCookies.AppendFormat("      Path: {0}\r\n", cookie.Path);
            //        sbCookies.AppendFormat("      Value: {0}\r\n", cookie.Value);
            //    }
            //}
            //sbCookies.AppendFormat("  --------<<<Request Cookies--------\r\n");
            //Log.Debug(sbCookies.ToString());
            #endregion

            #region Request Headers
            //StringBuilder sbHeaders = new StringBuilder();

            //sbHeaders.AppendFormat("--------Request Headers>>>--------\r\n");

            //foreach (string headerKey in Context.Request.Headers.AllKeys)
            //{
            //    sbHeaders.AppendFormat("    Header[\"{0}\"]\r\n", headerKey);
            //    string[] headerValues = Context.Request.Headers.GetValues(headerKey);
            //    sbHeaders.AppendFormat("    Value: {0}\r\n", string.Join(", ", headerValues));
            //}
            //sbHeaders.AppendFormat("  --------<<<Request Headers--------\r\n");
            //Log.Debug(sbHeaders.ToString());
            #endregion

        }
    }
}
