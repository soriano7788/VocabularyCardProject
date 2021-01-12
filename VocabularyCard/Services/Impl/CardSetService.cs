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
using System.Collections.Concurrent;

namespace VocabularyCard.Services.Impl
{
    public class CardSetService : BaseService, ICardSetService
    {
        private ConcurrentDictionary<int, string> _cacheCardSetName = new ConcurrentDictionary<int, string>();

        private ICardSetRepository _cardSetRepository;
        private ICardRepository _cardRepository;
        private CardSetConverter _cardSetConverter;

        private Guid _guid = Guid.NewGuid();

        public CardSetService(IUnitOfWork unitOfWork, ICardSetRepository cardSetRepository, ICardRepository cardRepository) : base(unitOfWork)
        {
            _cardSetRepository = cardSetRepository;
            _cardRepository = cardRepository;
            _cardSetConverter = new CardSetConverter();
        }

        /// <summary>
        /// cardSet 不存在，或是 cardSet於刪除狀態，皆會 throw exception
        /// 目前沒額外獨立權限判定的 method 或 class 設計
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public CardSetDto GetById(UserInfo user, int id)
        {
            CheckIsCardSetOwnerAndIsExist(user, id);
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
        public CardDto[] GetCardsByCardSetId(UserInfo user, int cardSetId)
        {
            CheckIsCardSetOwnerAndIsExist(user, cardSetId);
            ICollection<Card> cards = _cardRepository.GetByCardSetId(cardSetId);

            return new CardConverter().ToDataTransferObjects(cards.ToArray());
        }

        public string GetCardSetNameById(int cardSetId)
        {
            if (_cacheCardSetName.ContainsKey(cardSetId))
            {
                return _cacheCardSetName[cardSetId];
            }

            CardSet cardSet = _cardSetRepository.GetByCardSetId(cardSetId);
            if(cardSet == null)
            {
                throw new Exception("card set not found");
            }

            _cacheCardSetName.TryAdd(cardSetId, cardSet.DisplayName);

            return cardSet.DisplayName;
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
            
            return newCardSetDto;
        }

        public void DeleteById(UserInfo user, int cardSetId)
        {
            CheckIsCardSetOwnerAndIsExist(user, cardSetId);
            CardSet cardSet = _cardSetRepository.GetByCardSetId(cardSetId);
            cardSet.State = CardSetState.Removed;
            _cardSetRepository.Update(cardSet);
        }

        // todo: 此為暫時性 method，之後直接設計一個權限控管
        private void CheckIsCardSetOwnerAndIsExist(UserInfo user, int cardSetId)
        {
            CardSet cardSet = _cardSetRepository.GetByCardSetId(cardSetId);
            if (cardSet == null)
            {
                throw new ArgumentException("cardSetId not exist", "cardSetId");
            }
            if (cardSet.Owner != user.UserId)
            {
                throw new ArgumentException("user not cardSet owner", "userInfo");
            }
        }

    }
}
