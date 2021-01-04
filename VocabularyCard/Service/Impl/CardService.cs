using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Persistence;
using VocabularyCard.Domain;
using VocabularyCard.DTO;
using VocabularyCard.DTOConverter;
using VocabularyCard.Util;
using System.Runtime.InteropServices.WindowsRuntime;

namespace VocabularyCard.Service.Impl
{
    public class CardService: ICardService
    {
        private Guid _uniqueKey = Guid.NewGuid();
        public Guid UniqueKey
        {
            get
            {
                return _uniqueKey;
            }
        }
        public void ShowGuid()
        {
            LogUtility.ErrorLog("CardService guid: " + _uniqueKey);
        }

        private ICardDao _cardDao;

        private int _vocabularyLengthLimit;
        public int VocabularyLengthLimit
        {
            get { return _vocabularyLengthLimit; }
            set { _vocabularyLengthLimit = value; }
        }

        private ICardSetService _cardSetService;
        public ICardSetService CardSetService
        {
            get { return _cardSetService; }
            set { _cardSetService = value; }
        }

        public CardService() { }
        public CardService(ICardDao cardDao) 
        {
            _cardDao = cardDao;
        }

        public CardInfo GetById(int id)
        {
            Card card = _cardDao.Get(id);
            if(card != null)
            {
                CardInfo cardInfo = new CardConverter().ToDataTransferObject(card);
                return cardInfo;
            }

            return new CardInfo();
        }

        public CardInfo[] GetCardsByCardSetId(int cardSetId)
        {
            int defaultPageIndex = 0, 
                defaultPageSize = 1;

            return GetCardsByCardSetId(cardSetId, defaultPageIndex, defaultPageSize);
        }

        public CardInfo[] GetCardsByCardSetId(int cardSetId, int pageIndex, int pageSize)
        {
            //_cardDao.GetByCardSetId(cardSetId);
            throw new NotImplementedException();
        }

    }
}
