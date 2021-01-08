using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using VocabularyCard.DTO;

namespace VocabularyCard.Web.Controllers
{
    public class ValidationsController : Controller
    {
        // GET: Validations
        public ActionResult Index()
        {
            return View();
        }

        // 避免 cache，在 http get 下，response json 有可能被快取
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult UserName([Bind(Include = "DisplayName")] VUserInfo userInfo)
        {
            string message = null;
            if (!string.IsNullOrEmpty(userInfo.DisplayName))
            {
                const string prohibitedName = "fuck";

                if (userInfo.DisplayName == prohibitedName)
                {
                    message = "不可使用禁止名稱";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                message = "名稱不得空白";
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }


    }
}
