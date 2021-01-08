﻿using System;
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
            return _cardConverter.ToDataTransferObjects(cards.ToArray());
        }
    }
}
