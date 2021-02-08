using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.DTO;
using VocabularyCard.Core;
using VocabularyCard.Core.Services;
using VocabularyCard.Dtos;
using VocabularyCard.DtoConverters;
using VocabularyCard.Repositories;
using VocabularyCard.Entities;
using VocabularyCard.Util;

namespace VocabularyCard.Services.Impl
{
    public class CardService : BaseService, ICardService
    {
        private ICardRepository _cardRepository;
        private ICardSetRepository _cardSetRepository;
        private ICardInterpretationRepository _cardInterpretationRepository;
        private CardConverter _cardConverter;
        private CardInterpretationConverter _cardInterpretationConverter;

        public CardService(
            IUnitOfWork unitOfWork, 
            ICardRepository cardRepository,
            ICardSetRepository cardSetRepository,
            ICardInterpretationRepository cardInterpretationRepository) : base(unitOfWork)
        {
            _cardRepository = cardRepository;
            _cardSetRepository = cardSetRepository;
            _cardInterpretationRepository = cardInterpretationRepository;
            _cardConverter = new CardConverter();
            _cardInterpretationConverter = new CardInterpretationConverter();
        }

        public CardDto GetById(int cardId)
        {
            return GetById(cardId, false);
        }
        public CardDto GetById(int cardId, bool containDetail)
        {
            Card card = _cardRepository.GetByCardId(cardId);
            CardDto cardDto = _cardConverter.ToDataTransferObject(card);

            if (containDetail)
            {
                ICollection<CardInterpretation> interpretations = card.Interpretations;
                if (interpretations != null)
                {
                    cardDto.Interpretations = _cardInterpretationConverter.ToDataTransferObjects(interpretations.ToArray());
                }
                else
                {
                    LogUtility.DebugLog("interpretations 為 null");
                }
            }

            return cardDto;
        }
        public CardInterpretationDto[] GetCardInterpretations(int cardId)
        {
            IList<CardInterpretation> interpretations = _cardInterpretationRepository.GetByCardId(cardId);
            return _cardInterpretationConverter.ToDataTransferObjects(interpretations.ToArray());
        }

        public CardDto CreateCard(UserInfo user,int cardSetId, CardDto cardDto)
        {
            cardDto.Creator = user.UserId;
            cardDto.Modifier = user.UserId;
            cardDto.CreatedDateTime = DateTime.UtcNow;
            cardDto.ModifiedDateTime = DateTime.UtcNow;
            cardDto.State = CardState.Active;
            Card card = _cardConverter.ToEntity(cardDto);

            // 加入所屬的 CardSet
            card.CardSets = new List<CardSet>();
            CardSet cardSet = _cardSetRepository.GetByCardSetId(cardSetId);
            card.CardSets.Add(cardSet);

            Card newCard = _cardRepository.Create(card);
            UnitOfWork.Save();

            return _cardConverter.ToDataTransferObject(newCard);
        }
        public CardDto UpdateCard(UserInfo user, CardDto modifiedCardDto)
        {
            Card originalCard = _cardRepository.GetByCardId(modifiedCardDto.Id);
            CardDto originalCardDto = _cardConverter.ToDataTransferObject(originalCard);

            //Card modifiedCard = _cardConverter.ToEntity(modifiedCardDto);

            // 比對就以 dto 的狀態來進行吧
            // CardInterpretaion 也加上 State 來處理 Remove 吧。
            // 因為誤刪的話很麻煩


            // 要加上一次 新增、刪除、修改 多筆 Interpretations 的 sql 操作


            // 這裡要手動 unitOfWork Commit
            // commit 後再轉回 dto return
            // 就算轉換過程 exception 也還不是大問題

            if(originalCard.Vocabulary.Trim() != originalCardDto.Vocabulary.Trim())
            {
                originalCard.Vocabulary = originalCardDto.Vocabulary.Trim();
                _cardRepository.Update(originalCard);
            }


            #region add interprets
            CardInterpretationDto[] newInterprets = FilterNewInterpretations(modifiedCardDto.Interpretations);
            foreach(CardInterpretationDto interpret in newInterprets)
            {
                interpret.CardId = originalCard.CardId;
            }
            CardInterpretation[] newInterpretEntities = _cardInterpretationConverter.ToEntities(newInterprets);
            _cardInterpretationRepository.CreateMany(newInterpretEntities);
            #endregion

            #region modified interprets
            CardInterpretationDto[] modifiedInterprets = FilterModifiedInterpretations(originalCardDto.Interpretations, modifiedCardDto.Interpretations);
            foreach (CardInterpretationDto interpret in modifiedInterprets)
            {
            }
            #endregion

            #region remove interprets
            CardInterpretationDto[] removedInterprets = FilterRemovedInterpretations(originalCardDto.Interpretations, modifiedCardDto.Interpretations);
            foreach (CardInterpretationDto interpret in removedInterprets)
            {
                interpret.State = CardInterpretationState.Removed;
            }
            #endregion

            #region [Debug]
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.AppendFormat("New{0}", Environment.NewLine);
            foreach (var i in newInterprets)
            {
                sb.AppendFormat("解釋: {0}{1}", i.Interpretation, Environment.NewLine);
            }
            sb.Append(Environment.NewLine);

            sb.AppendFormat("Modified{0}", Environment.NewLine);
            foreach (var i in modifiedInterprets)
            {
                sb.AppendFormat("解釋: {0}{1}", i.Interpretation, Environment.NewLine);
            }
            sb.Append(Environment.NewLine);

            sb.AppendFormat("Removed{0}", Environment.NewLine);
            foreach (var i in removedInterprets)
            {
                sb.AppendFormat("解釋: {0}{1}", i.Interpretation, Environment.NewLine);
            }
            sb.Append(Environment.NewLine);

            LogUtility.DebugLog(sb.ToString());
            #endregion

            //UnitOfWork.Save();

            return modifiedCardDto;
        }

        public CardDto[] GetCardsByCardSetId(UserInfo userInfo, int cardSetId)
        {
            // todo: 要先檢查 user 是否為 cardSet 的 owner
            // 這樣就又要用到 _cardSetRepository 了。
            // 是否該把 owner 這件事改成一個 Permission 的概念

            CardSet cardSet = _cardSetRepository.GetByCardSetId(cardSetId);
            if (cardSet == null)
            {
                throw new ArgumentException("cardSetId not exist", "cardSetId");
            }
            if (cardSet.Owner != userInfo.UserId)
            {
                throw new ArgumentException("user not cardSet owner", "userInfo");
            }

            IList<Card> cards = _cardRepository.GetByCardSetId(cardSetId);

            // todo: 假如這種 case 很普遍的話，是否應該在另外寫一個 method 來處理這種 case? 
            // 記得要關掉 lazy loading，不然光是用個 dot 存取運算子就會觸發 lazy loading

            // 手動轉換....
            List<CardDto> cardDtos = new List<CardDto>();
            foreach(Card card in cards)
            {
                CardDto cardDto = _cardConverter.ToDataTransferObject(card);
                CardInterpretationDto[] interpretationDtos = _cardInterpretationConverter.ToDataTransferObjects(card.Interpretations.ToArray());
                cardDto.Interpretations = interpretationDtos;
                cardDtos.Add(cardDto);
            }

            return cardDtos.ToArray();
            //_cardConverter.ToDataTransferObjects(cards.ToArray());

            //return _cardConverter.ToDataTransferObjects(cards.ToArray());
        }


        private CardInterpretationDto[] FilterNewInterpretations(CardInterpretationDto[] modifiedInterpretations)
        {
            // modified id is 0
            return modifiedInterpretations.Where(i => i.Id == 0).ToArray();
        }
        private CardInterpretationDto[] FilterModifiedInterpretations(CardInterpretationDto[] originalInterpretations, CardInterpretationDto[] modifiedInterpretations)
        {
            // both origin and modified exist
            // 其實也不用先過濾出同 ID 的
            //var modifiedSameIds = modifiedInterpretations.Where(m => m.Id != 0 && originalInterpretations.Count(o => o.Id == m.Id) == 1);

            List<CardInterpretationDto> modifiedList = new List<CardInterpretationDto>();
            // IEnumerable 和 Array、 ArrayList、List 在 Count() 等等 methods 操作上有何差異?
            foreach(CardInterpretationDto modified in modifiedInterpretations)
            {
                foreach(CardInterpretationDto origin in originalInterpretations)
                {
                    if(modified.Id == origin.Id)
                    {
                        if(IsEqualInterpretation(modified,origin))
                        {
                            modifiedList.Add(modified);
                        }
                        break;
                    }
                }
            }
            return modifiedList.ToArray();
        }
        private CardInterpretationDto[] FilterRemovedInterpretations(CardInterpretationDto[] originalInterpretations, CardInterpretationDto[] modifiedInterpretations)
        {
            // origin not exist, but modified exist
            return originalInterpretations.Where(o => modifiedInterpretations.Count(m => m.Id == o.Id) == 0).ToArray();

            //return modifiedInterpretations.Where(m => m.Id != 0 && originalInterpretations.Count(o => o.Id == m.Id) == 0).ToArray();
        }

        /// <summary>
        /// 比對兩個 Interpretation
        /// </summary>
        /// <returns></returns>
        private bool IsEqualInterpretation(CardInterpretationDto a , CardInterpretationDto b)
        {
            // 只比對以下 5 項
            // PartOfSpeech
            // PhoneticSymbol
            // Interpretation
            // ExampleSentence
            // ExampleSentenceExplanation

            if (a.PartOfSpeech != b.PartOfSpeech)
                return false;

            if (a.PhoneticSymbol != b.PhoneticSymbol)
                return false;

            if (a.Interpretation != b.Interpretation)
                return false;

            if (a.ExampleSentence != b.ExampleSentence)
                return false;

            if (a.ExampleSentenceExplanation != b.ExampleSentenceExplanation)
                return false;

            return true;
        }



    }
}
