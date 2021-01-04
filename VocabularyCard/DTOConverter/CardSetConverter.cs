using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;
using VocabularyCard.DTO;
using VocabularyCard.Util;

namespace VocabularyCard.DTOConverter
{
    public class CardSetConverter
    {
        public CardSet ToDomainObject(CardSetInfo cardSetInfo)
        {
            CardSet cardSet = new CardSet();
            cardSet.CardSetId = cardSetInfo.Id;
            cardSet.Flag = cardSetInfo.Flag;
            cardSet.DisplayName = cardSetInfo.DisplayName;
            cardSet.Description = cardSetInfo.Description;
            cardSet.Creator = cardSetInfo.Creator;
            cardSet.Modifier = cardSetInfo.Modifier;
            cardSet.Owner = cardSetInfo.Owner;
            cardSet.CreatedDateTime = cardSetInfo.CreatedDateTime;
            cardSet.ModifiedDateTime = cardSetInfo.ModifiedDateTime;
            cardSet.State = cardSetInfo.State;

            return cardSet;
        }
        public CardSet[] ToDomainObjects(CardSetInfo[] cardSetInfos)
        {
            List<CardSet> cardSets = new List<CardSet>();
            foreach(CardSetInfo cardSetInfo in cardSetInfos)
            {
                cardSets.Add(ToDomainObject(cardSetInfo));
            }

            return cardSets.ToArray();
        }

        public CardSetInfo ToDataTransferObject(CardSet cardSet)
        {
            // 好像序列化過程就會去存取 Cards 屬性???
            // 只能直接側錄 sql server 看看了，看到底送了啥 sql，
            // 是一開始就抓全部，還是後來才抓 Cards




            //LogUtility.ErrorLog("before: " + JsonConvert.SerializeObject(cardSet, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            //if (cardSet.Cards == null)
            //{ }
            //LogUtility.ErrorLog("after: " + JsonConvert.SerializeObject(cardSet, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));



            CardSetInfo cardSetInfo = new CardSetInfo();
            cardSetInfo.Id = cardSet.CardSetId;
            cardSetInfo.Flag = cardSet.Flag;
            cardSetInfo.DisplayName = cardSet.DisplayName;
            cardSetInfo.Description = cardSet.Description;
            cardSetInfo.Creator = cardSet.Creator;
            cardSetInfo.Modifier = cardSet.Modifier;
            cardSetInfo.Owner = cardSet.Owner;
            cardSetInfo.CreatedDateTime = cardSet.CreatedDateTime;
            cardSetInfo.ModifiedDateTime = cardSet.ModifiedDateTime;
            cardSetInfo.State = cardSet.State;


            //List<CardInfo> cards = new List<CardInfo>();
            //if(cardSet.Cards != null)
            //{
            //    var cardConverter = new CardConverter();
            //    foreach(Card card in cardSet.Cards)
            //    {
            //        cards.Add(cardConverter.ToDataTransferObject(card));
            //    }
            //}
            //cardSetInfo.Cards = cards.ToArray();

            return cardSetInfo;
        }
        public CardSetInfo[] ToDataTransferObjects(CardSet[] cardSets) 
        {
            List<CardSetInfo> cardSetInfos = new List<CardSetInfo>();
            foreach (CardSet cardSet in cardSets)
            {
                cardSetInfos.Add(ToDataTransferObject(cardSet));
            }

            return cardSetInfos.ToArray();
        }
    }
}
