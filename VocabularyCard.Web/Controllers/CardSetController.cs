using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using VocabularyCard.Dtos;
using VocabularyCard.Services;
using VocabularyCard.Web.Filters;

namespace VocabularyCard.Web.Controllers
{
    //[OutputCache(CacheProfile = "CardSetCache")]
    public class CardSetController : Controller
    {
        private ICardService _cardService;
        private ICardSetService _cardSetService;

        public CardSetController(ICardSetService cardSetService, ICardService cardService)
        {
            _cardSetService = cardSetService;
            _cardService = cardService;
        }

        // 只針對此 controller 自訂驗證規則
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);
        }
        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            base.OnAuthenticationChallenge(filterContext);
        }

        [Auth]
        public ActionResult GetAll()
        {
            CardSetDto[] cardSetInfos = _cardSetService.GetAll();

            if (Request.IsAjaxRequest())
            {
                if (cardSetInfos == null)
                {
                    // 內部沒任何實作，可參考 null object pattern
                    return new EmptyResult();
                }

                // todo: AllowGet 有資安風險，請參考 Json() 的說明
                //return Json(cardSetInfos, JsonRequestBehavior.DenyGet);

                return new Extensions.AjaxJsonResult(true, cardSetInfos);
            }
            else
            {
                return View();
            }
        }

        // GET: CardSet
        public ActionResult Index()
        {
            return View();
        }

        //[Mvc5Authv1]
        //[RequireHttps] // 當 request為 http 時，會 response 302，把 protocol 改成 https
        [Mvc5Authv2]
        [OverrideAuthentication()]
        [CardSetActionFilter]
        [CardSetResultFilter]
        //[OutputCache(Duration = 10)]
        public ActionResult List()
        {
            //throw new NotImplementedException();
            var logger = NLog.LogManager.GetLogger("TEST");
            logger.Error("123");

            Global.Log.Debug("List 1");

            //ICardSetService zzz = new RepositoryFactory().GetCardSetService();
            //ICardSetService zzz = WebUtility.Repository.GetCardSetService();
            //CardSetInfo[] cardSetInfos = zzz.GetAll();

            CardSetDto[] cardSetDtos = _cardSetService.GetAll();
            CardDto cardDto = _cardService.GetById(1);

            ViewData["testViewDataString"] = "testZZZ";
            ViewBag.CardSets = cardSetDtos;
            ViewBag.i18nCard = Resources.VocabularyCard.Card;

            //ViewData.Model = cardSetInfos;
            //TempData["test"] = "abc";

            Tuple<CardSetDto[], CardDto> tupleModel = Tuple.Create<CardSetDto[], CardDto>(cardSetDtos, cardDto);
            Tuple<CardSetDto[], CardDto> tupleModel2 = new Tuple<CardSetDto[], CardDto>(cardSetDtos, cardDto);
            ViewData.Model = tupleModel;

            Global.Log.Debug("List 2");



            #region test entity framework many to many

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("===========================");
            CardSetDto cardSetDto = _cardSetService.GetById(1);
            //sb.AppendLine(JsonConvert.SerializeObject(cardSetInfo));
            ////Global.Log.Error("cardsetInfo.Cards.Length: " + cardsetInfo.Cards.Length);
            //sb.AppendLine("===========================");
            //Global.Log.Error(sb.ToString());

            #endregion

            // 假如是 ajax request 就回傳 jaon，否則就回傳一個頁面
            if (Request.IsAjaxRequest())
            {
                if (cardSetDtos == null)
                {
                    // 內部沒任何實作，可參考 null object pattern
                    return new EmptyResult();
                }


                // todo: AllowGet 有資安風險，請參考 Json() 的說明
                return Json(cardSetDtos, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return View(cardSetDtos);
            }
        }

        public ActionResult List2()
        {
            ViewBag.ABC = "DEF";
            return View("List2");
        }

        public ActionResult Detail(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "id 不可小於等於 0");
            }

            return View();
        }

        // Flag, CreatedDateTime, ModifiedDateTime 欄位不要被自動 binding，直接設定在 model 上會比較方便
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Flag,CreatedDateTime,ModifiedDateTime")] CardSetDto cardSetInfo)
        {
            // cardSetInfo 驗證通過的意思
            if (ModelState.IsValid)
            {
                // 自訂驗證
                if (cardSetInfo.DisplayName == "fuck")
                {
                    ModelState.AddModelError("DisplayName", "顯示名稱含不雅文字");
                    return View(cardSetInfo);
                }

                _cardSetService.Create(cardSetInfo);

                return RedirectToAction("List");
            }
            else
            {
                if (ModelState["DisplayName"].Errors.Count > 0)
                {
                    foreach (ModelError error in ModelState["DisplayName"].Errors)
                    {
                        // 寫 log 之類的
                    }
                }

                //foreach (ModelState modelState in ModelState.Values)
                //{
                //}

                // 不希望 ModelState 被傳回 View 的話，這邊用 Clear 可以清除 ModelState 字典
                // ModelState.Clear();
                return View(cardSetInfo);
            }

        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return RedirectToAction("List");
        }

        public ActionResult ResponseJsonResult()
        {
            CardSetDto[] cardSetInfos = _cardSetService.GetAll();
            return Json(cardSetInfos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCardSetJson(CardSetDto cardSetInfo)
        {
            return Json(cardSetInfo, JsonRequestBehavior.DenyGet);
        }

        // 指定名稱為 Error 的 View
        [HandleError(View = "Error", ExceptionType = typeof(Exception))]
        public ActionResult TestException()
        {
            throw new Exception("測試 Error 頁面");
            return View();
        }

    }
}
