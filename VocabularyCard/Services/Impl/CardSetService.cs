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
    public class CardSetService : BaseService, ICardSetService
    {
        private ICardSetRepository _cardSetRepository;
        private ICardRepository _cardRepository;
        private CardSetConverter _cardSetConverter;

        public CardSetService(IUnitOfWork unitOfWork, ICardSetRepository cardSetRepository, ICardRepository cardRepository) : base(unitOfWork)
        {
            _cardSetRepository = cardSetRepository;
            _cardRepository = cardRepository;
            _cardSetConverter = new CardSetConverter();
        }

        public CardSetDto GetById(int id)
        {
            CardSet cardSet = _cardSetRepository.GetByCardSetId(id);
            return _cardSetConverter.ToDataTransferObject(cardSet);
        }
        public CardSetDto[] GetAll()
        {
            IList<CardSet> cardSets = _cardSetRepository.GetAll();
            return _cardSetConverter.ToDataTransferObjects(cardSets.ToArray());
        }
        public CardSetDto[] GetByOwner(UserInfo owner) 
        {
            IList<CardSet> cardSets = _cardSetRepository.GetByOwner(owner.UserId);
            return _cardSetConverter.ToDataTransferObjects(cardSets.ToArray());
        }
        public CardDto[] GetCardsByCardSetId(UserInfo userInfo, int cardSetId)
        {
            // todo: 這個 method 或是放在 cardService 會比較恰當?
            // 以高內聚的設計準則來思考，_cardRepository 大概也只有這個 method 會用到
            // 移到 cardService 應該比較適合

            CardSet cardSet = _cardSetRepository.GetByCardSetId(cardSetId);
            if(cardSet == null)
            {
                throw new ArgumentException("cardSetId not exist", "cardSetId");
            }
            if(cardSet.Owner != userInfo.UserId)
            {
                throw new ArgumentException("user not cardSet owner", "userInfo");
            }

            ICollection<Card> cards = _cardRepository.GetByCardSetId(cardSetId);

            //// todo: 這邊沒把 interpretation 轉換出來
            //foreach(Card card in cards)
            //{ 
            //}

            return new CardConverter().ToDataTransferObjects(cards.ToArray());
        }

        public CardSetDto Create(UserInfo user, CardSetDto cardSetDto)
        {
            cardSetDto.Creator = user.UserId;
            cardSetDto.Modifier = user.UserId;
            cardSetDto.Owner = user.UserId;
            cardSetDto.CreatedDateTime = DateTime.UtcNow;
            cardSetDto.ModifiedDateTime = DateTime.UtcNow;
            cardSetDto.State = CardSetState.Active;

            CardSet entity = _cardSetConverter.ToEntity(cardSetDto);
            CardSet newEntity = _cardSetRepository.Create(entity);
            CardSetDto newCardSetDto = _cardSetConverter.ToDataTransferObject(newEntity);
            
            UnitOfWork.Save();

            return newCardSetDto;
        }
    }
}
