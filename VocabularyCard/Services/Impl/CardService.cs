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

namespace VocabularyCard.Services.Impl
{
    public class CardService : BaseService, ICardService
    {
        private ICardRepository _cardRepository;
        private CardConverter _cardConverter;

        public CardService(IUnitOfWork unitOfWork, ICardRepository cardRepository) : base(unitOfWork)
        {
            _cardRepository = cardRepository;
            _cardConverter = new CardConverter();
        }

        public CardDto[] GetCardsByCardSetId(UserInfo userInfo, int cardSetId)
        {
            // todo: 要先檢查 user 是否為 cardSet 的 owner
            // 這樣就又要用到 _cardSetRepository 了。
            // 是否該把 owner 這件事改成一個 Permission 的概念

            IList<Card> cards = _cardRepository.GetByCardSetId(cardSetId);
            return _cardConverter.ToDataTransferObjects(cards.ToArray());
        }
    }
}
