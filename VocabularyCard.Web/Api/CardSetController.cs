using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VocabularyCard;
using VocabularyCard.Services;
using VocabularyCard.Dtos;
using VocabularyCard.Util;
using VocabularyCard.AccountManager.DTO;
using VocabularyCard.Web.Filters;
using VocabularyCard.Web.Util;

namespace VocabularyCard.Web.Api
{
    public class CardSetController : ApiController
    {
        private ICardSetService _cardSetService;

        public CardSetController(ICardSetService cardSetService)
        {
            _cardSetService = cardSetService;
        }

        //[IgnoreFilter(typeof(ApiResultAttribute))]
        public CardSetDto[] GetAll()
        {
            CardSetDto[] cardSetDtos = _cardSetService.GetAll();
            return cardSetDtos;
        }

        public CardSetDto[] GetByOwner()
        {
            UserInfo user = Util.WebUtility.GetCurrentUser();
            CardSetDto[] cardSetDtos = _cardSetService.GetByOwner(user);
            return cardSetDtos;
        }

        [Route("api/cardset/GetCards/{cardSetId}")]
        [HttpGet]
        public CardDto[] GetCards(int cardSetId)
        {
            UserInfo user = Util.WebUtility.GetCurrentUser();
            CardDto[] cardDtos = _cardSetService.GetCardsByCardSetId(user, cardSetId);
            return cardDtos;
        }

        [Route("api/CardSet/Create")]
        [HttpPost]
        public CardSetDto Create([FromBody] CardSetDto cardSet)
        {
            UserInfo user = Util.WebUtility.GetCurrentUser();
            return _cardSetService.Create(user, cardSet);
        }

        [AllowAnonymous]
        [HttpPost]
        public void Test()
        {
            //var context = new DbContextBase();
            //context.Database.CommandTimeout = 30;

            //_cardSetService.TestUnitOfWork();


            //_testCardSetService.TestUnitOfWork();
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        // https://127.0.0.1/VocabularyCard/api/CardSet/get?id=1
        [HttpGet]
        public CardSetDto Get(int id)
        {
            var user = Util.WebUtility.GetCurrentUser();
            CardSetDto cardSetDto = _cardSetService.GetById(user, id);
            return cardSetDto;
        }

        [Route("api/CardSet/GetName/{cardSetId}")]
        [HttpGet]
        public string GetName(int cardSetId)
        {
            string cardSetName = _cardSetService.GetCardSetNameById(cardSetId);
            return cardSetName;
        }

        // DELETE api/<controller>/5
        [Route("api/CardSet/Delete/{cardSetId}")]
        [HttpDelete]
        public void Delete(int cardSetId)
        {
            UserInfo user = Util.WebUtility.GetCurrentUser();
            _cardSetService.DeleteById(user, cardSetId);
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }


    }
}
