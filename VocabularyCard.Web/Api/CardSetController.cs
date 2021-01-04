using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VocabularyCard;
using VocabularyCard.Service;
using VocabularyCard.Persistence.EF;
using VocabularyCard.DTO;
using VocabularyCard.Util;
using VocabularyCard.AccountManager.DTO;
using VocabularyCard.Web.Filters;
using VocabularyCard.Web.Util;

namespace VocabularyCard.Web.Api
{
    public class CardSetController : ApiController
    {
        private ICardSetService _cardSetService;
        private VocabularyCard.Test.Service.ICardSetService _testCardSetService;

        public CardSetController(ICardSetService cardSetService, VocabularyCard.Test.Service.ICardSetService testCardSetService)
        {
            _cardSetService = cardSetService;
            _testCardSetService = testCardSetService;
        }

        //[IgnoreFilter(typeof(ApiResultAttribute))]
        //[AllowAnonymous]
        public CardSetInfo[] GetAll()
        {
            CardSetInfo[] cardSetInfos = _cardSetService.GetAll();
            return cardSetInfos;
        }

        public CardSetInfo[] GetByOwner()
        {
            UserInfo user = Util.WebUtility.GetCurrentUser();
            CardSetInfo[] cardSetInfos = _cardSetService.GetByOwner(user);
            return cardSetInfos;
        }

        public CardInfo[] GetCards(int cardSetId)
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        [HttpPost]
        public void Test()
        {
            //var context = new DbContextBase();
            //context.Database.CommandTimeout = 30;

            //_cardSetService.TestUnitOfWork();


            _testCardSetService.TestUnitOfWork();
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        // https://127.0.0.1/VocabularyCard/api/CardSet/get?id=1
        [HttpPost]
        public CardSetInfo Get(int id)
        {
            _cardSetService = new RepositoryFactory().GetCardSetService();
            CardSetInfo cardSetInfo = _cardSetService.GeyById(id);

            return cardSetInfo;
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
