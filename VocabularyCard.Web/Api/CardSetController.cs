using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VocabularyCard;
using VocabularyCard.Services;
using VocabularyCard.Persistence.EF;
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
        //[AllowAnonymous]
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

        public CardSetDto[] GetCards(int cardSetId)
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


            //_testCardSetService.TestUnitOfWork();
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        // https://127.0.0.1/VocabularyCard/api/CardSet/get?id=1
        [HttpPost]
        public CardSetDto Get(int id)
        {
            CardSetDto cardSetDto = _cardSetService.GetById(id);
            return cardSetDto;
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
