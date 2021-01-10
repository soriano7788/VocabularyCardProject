using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.DTO;
using VocabularyCard.Core.Services;
using VocabularyCard.Dtos;

namespace VocabularyCard.Services
{
    public interface ICardSetService : IService
    {
        CardSetDto GetById(UserInfo userInfo, int id);
        CardSetDto[] GetAll();
        CardSetDto[] GetByOwner(UserInfo owner);
        CardDto[] GetCardsByCardSetId(UserInfo userInfo, int cardSetId);
        string GetCardSetNameById(int cardSetId);
        CardSetDto Create(UserInfo user, CardSetDto cardSetInfo);

        void DeleteById(UserInfo user, int cardSetId);
    }
}
