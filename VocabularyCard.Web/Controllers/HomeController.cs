using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VocabularyCard.Web.Filters;

namespace VocabularyCard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[AuthorizePlus]
        [AuthorizePlus2]
        public ActionResult Backend()
        {
            return Content("您已經進入後台");
        }

        #region session auth example
        public ActionResult Login()
        {
            Session["auth"] = true;
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            Session["auth"] = false;
            return RedirectToAction("Index");
        }
        #endregion

        #region cookie auth example
        public ActionResult Login2(string account, string password)
        {
            string cookieName = "mvcAuth";
            if (account == "mvc" && password == "123456")
            {
                if (Response.Cookies.AllKeys.Contains(cookieName))
                {
                    string cookieVal = Response.Cookies[cookieName].Value;
                    // 這是 remove 啥????
                    HttpContext.Application.Remove(cookieVal);

                    // 這就很清楚是清除舊 cookie
                    Response.Cookies.Remove(cookieName);
                }

                // 生一組新 token
                string token = Guid.NewGuid().ToString();
                // 將 token 存放到 Application 內(實務上應該存進資料庫)    沒碰過這個目前不了解這是啥
                HttpContext.Application[token] = DateTime.UtcNow.AddHours(1);

                var hc = new HttpCookie(cookieName, token)
                {
                    Expires = DateTime.Now.AddHours(1),
                    HttpOnly = true
                };
                Response.Cookies.Add(hc);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Forms Authentication 驗證
        public ActionResult Login3(string account, string password)
        {
            if (account == "mvc" && password == "123456")
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    account,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    true,
                    "userdata",
                    FormsAuthentication.FormsCookiePath);

                string encTicket = FormsAuthentication.Encrypt(ticket);

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

                cookie.HttpOnly = true;

                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Test()
        {
            return Content("登入成功");
        }
        #endregion

    }
}