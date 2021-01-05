using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Entities;
using VocabularyCard.Dtos;

namespace VocabularyCard.DtoConverters
{
    public class CardConverter
    {
        public Card ToEntity(CardDto dto)
        {
            Card card = new Card();
            card.CardId = dto.Id;
            card.Vocabulary = dto.Vocabulary;
            card.Creator = dto.Creator;
            card.Modifier = dto.Modifier;
            card.CreatedDateTime = dto.CreatedDateTime;
            card.ModifiedDateTime = dto.ModifiedDateTime;
            card.State = dto.State;

            return card;
        }
        public Card[] ToEntities(CardDto[] dtos)
        {
            List<Card> cards = new List<Card>();
            foreach (CardDto cardInfo in dtos)
            {
                cards.Add(ToEntity(cardInfo));
            }

            return cards.ToArray();
        }

        public CardDto ToDataTransferObject(Card card)
        {
            CardDto cardDto = new CardDto();
            cardDto.Id = card.CardId;
            cardDto.Vocabulary = card.Vocabulary;
            cardDto.Creator = card.Creator;
            cardDto.Modifier = card.Modifier;
            cardDto.CreatedDateTime = card.CreatedDateTime;
            cardDto.ModifiedDateTime = card.ModifiedDateTime;
            cardDto.State = card.State;

            return cardDto;
        }
        public CardDto[] ToDataTransferObjects(Card[] cards)
        {
            List<CardDto> cardDto = new List<CardDto>();
            foreach (Card card in cards)
            {
                cardDto.Add(ToDataTransferObject(card));
            }

            return cardDto.ToArray();
        }
    }
}
