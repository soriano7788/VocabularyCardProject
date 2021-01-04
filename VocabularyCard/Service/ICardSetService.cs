using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.DTO;
using VocabularyCard.DTO;

namespace VocabularyCard.Service
{
    public interface ICardSetService
    {
        CardSetInfo GeyById(int id);
        CardSetInfo[] GetAll();
        CardSetInfo[] GetByOwner(UserInfo owner);
        CardInfo[] GetCardsByCardSetId(UserInfo userInfo, int cardSetId);

        CardSetInfo Create(CardSetInfo cardSetInfo);

        void TestUnitOfWork();
    }
}
