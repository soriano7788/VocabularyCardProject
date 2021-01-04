using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;
using VocabularyCard.DTO;
using VocabularyCard.DTOConverter;
using VocabularyCard.Persistence;
using VocabularyCard.Util;
using VocabularyCard.AccountManager.DTO;

namespace VocabularyCard.Service.Impl
{
    public class CardSetService: ICardSetService
    {
        private Guid _uniqueKey = Guid.NewGuid();
        public Guid UniqueKey
        {
            get
            {
                return _uniqueKey;
            }
        }
        private ICardSetDao _cardSetDao;
        private ICardDao _cardDao;

        //private IUnitOfWork _unitOfWork;
        //private IRepository<CardSet> _repository;

        public CardSetService(ICardSetDao cardSetDao, ICardDao cardDao/*, IUnitOfWork unitOfWork*/)
        {
            _cardSetDao = cardSetDao;
            _cardDao = cardDao;
            //_unitOfWork = unitOfWork;
            //_repository = _unitOfWork.Repository<CardSet>();
        }

        public CardSetInfo GeyById(int id)
        {
            CardSet cardSet = _cardSetDao.Get(id);
            return new CardSetConverter().ToDataTransferObject(cardSet);
        }

        public CardSetInfo[] GetAll()
        {
            CardSet[] cardSets = _cardSetDao.GetAll();
            CardSetInfo[] cardSetInfos = new CardSetConverter().ToDataTransferObjects(cardSets);

            return cardSetInfos;
        }
        public CardSetInfo[] GetByOwner(UserInfo owner)
        {
            CardSet[] cardSets = _cardSetDao.GetByOwner(owner.UserId);
            CardSetInfo[] cardSetInfos = new CardSetConverter().ToDataTransferObjects(cardSets);

            return cardSetInfos;
        }
        public CardInfo[] GetCardsByCardSetId(UserInfo userInfo, int cardSetId)
        {
            // 檢查 user 是否為 owner
            throw new NotImplementedException();
        }


        public CardSetInfo Create(CardSetInfo cardSetInfo) 
        {
            CardSetConverter converter = new CardSetConverter();
            CardSet cardSet = _cardSetDao.Create(converter.ToDomainObject(cardSetInfo));
            return converter.ToDataTransferObject(cardSet);
        }


        public void TestUnitOfWork()
        {
            //CardSet cardSet = new CardSet
            //{
            //    Flag = 1,
            //    DisplayName = "Test Unit Of Work",
            //    Description = string.Empty,
            //    CreatedDateTime = DateTime.UtcNow,
            //    ModifiedDateTime = DateTime.UtcNow,
            //    Creator = "test",
            //    Modifier = "test",
            //    Owner = "test",
            //    State = CardSetState.Active
            //};
            //_cardSetDao.Create(cardSet);

            //throw new Exception("test unit of work");

            //Card card = new Card
            //{
            //    Flag = 1,
            //    Vocabulary = "Test Unit Of Work",
            //    CreatedDateTime = DateTime.UtcNow,
            //    ModifiedDateTime = DateTime.UtcNow,
            //    Creator = "test",
            //    Modifier = "test",
            //    State = CardState.Active
            //};
            //_cardDao.Create(card);

            //_unitOfWork.Save();
        }

    }
}
