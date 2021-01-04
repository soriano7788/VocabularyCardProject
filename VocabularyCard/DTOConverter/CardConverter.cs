using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;
using VocabularyCard.DTO;

namespace VocabularyCard.DTOConverter
{
    public class CardConverter
    {
        public Card ToDomainObject(CardInfo cardInfo)
        {
            Card card = new Card();
            card.CardId = cardInfo.Id;
            card.Flag = cardInfo.Flag;
            card.Vocabulary = cardInfo.Vocabulary;
            card.Creator = cardInfo.Creator;
            card.Modifier = cardInfo.Modifier;
            card.CreatedDateTime = cardInfo.CreatedDateTime;
            card.ModifiedDateTime = cardInfo.ModifiedDateTime;
            card.State = cardInfo.State;

            return card;
        }
        public Card[] ToDomainObjects(CardInfo[] cardInfos)
        {
            List<Card> cards = new List<Card>();
            foreach (CardInfo cardInfo in cardInfos)
            {
                cards.Add(ToDomainObject(cardInfo));
            }

            return cards.ToArray();
        }

        public CardInfo ToDataTransferObject(Card card)
        {
            CardInfo cardInfo = new CardInfo();
            cardInfo.Id = card.CardId;
            cardInfo.Flag = card.Flag;
            cardInfo.Vocabulary = card.Vocabulary;
            cardInfo.Creator = card.Creator;
            cardInfo.Modifier = card.Modifier;
            cardInfo.CreatedDateTime = card.CreatedDateTime;
            cardInfo.ModifiedDateTime = card.ModifiedDateTime;
            cardInfo.State = card.State;

            return cardInfo;
        }
        public CardInfo[] ToDataTransferObjects(Card[] cards)
        {
            List<CardInfo> cardInfos = new List<CardInfo>();
            foreach (Card card in cards)
            {
                cardInfos.Add(ToDataTransferObject(card));
            }

            return cardInfos.ToArray();
        }
    }
}
