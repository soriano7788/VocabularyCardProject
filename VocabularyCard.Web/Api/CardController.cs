using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VocabularyCard.Services;
using VocabularyCard.Dtos;
using VocabularyCard.AccountManager.DTO;

namespace VocabularyCard.Web.Api
{
    public class CardController : ApiController
    {
        private ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [Route("api/card/create/{cardSetId}")]
        [HttpPost]
        public CardDto Create(int cardSetId, [FromBody]CardDto cardDto)
        {
            UserInfo user = Util.WebUtility.GetCurrentUser();
            return _cardService.CreateCard(user, cardSetId, cardDto);
        }

        [Route("api/card/update/")]
        [HttpPut]
        public CardDto Update([FromBody] CardDto cardDto)
        {
            UserInfo user = Util.WebUtility.GetCurrentUser();
            return _cardService.UpdateCard(user, cardDto);
        }

        [Route("api/card/GetByCardSetId/{cardSetId}")]
        [HttpGet]
        public CardDto[] GetCardsByCardSetId(int cardSetId)
        {
            UserInfo user = Util.WebUtility.GetCurrentUser();
            return _cardService.GetCardsByCardSetId(user, cardSetId);
        }

        [Route("api/card/{cardId}/{containDetail?}")]
        [HttpGet]
        public CardDto Get(int cardId, bool containDetail = false)
        {
            return _cardService.GetById(cardId, containDetail);
        }

        [Route("api/card/interpretations/{cardId}")]
        [HttpGet]
        public CardInterpretationDto[] GetInterpretations(int cardId)
        {
            return _cardService.GetCardInterpretations(cardId);
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
