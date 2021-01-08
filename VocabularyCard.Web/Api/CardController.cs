using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VocabularyCard.Services;
using VocabularyCard.Dtos;

namespace VocabularyCard.Web.Api
{
    public class CardController : ApiController
    {
        private ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [Route("api/card/{cardId}/{containDetail?}")]
        [HttpGet]
        [AllowAnonymous]
        public CardDto Get(int cardId, bool containDetail = false)
        {
            return _cardService.GetById(cardId, containDetail);
        }

        [Route("api/card/interpretations/{cardId}")]
        [HttpGet]
        [AllowAnonymous]
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
