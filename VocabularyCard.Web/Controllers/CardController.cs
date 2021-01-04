using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VocabularyCard.DTO;
using VocabularyCard.Service;

namespace VocabularyCard.Web.Controllers
{
    // 改繼承 AsyncController，MVC4 開始之後就不太用這種 Controller 了?
    // 後來大多直接用 async await 來方便使用?
    public class CardController : Controller
    {
        private ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        // GET: Card
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Detail(int id)
        {
            //if(id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            // service return 的資料型態必須是 Task<CardInfo> 才能使用 await，
            // 所以，假如 method 的回傳型態是 Task<Type>
            // 那此 method 是一個非同步 method????
            //CardInfo cardInfo = await _cardService.GetById(id);
            CardInfo cardInfo = _cardService.GetById(id);
            ViewData.Model = cardInfo;

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            return View();
        }

        //[AsyncTimeout(10000)]
        //public void DownloadAsync(string url)
        //{ }
        //public ActionResult DownloadCompleted(string content)
        //{
        //    return Content(content);
        //}
    }
}