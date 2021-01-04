using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.DTO;

namespace VocabularyCard.Service
{
    public interface ICardService
    {
        CardInfo GetById(int id);
        CardInfo[] GetCardsByCardSetId(int cardSetId);
        CardInfo[] GetCardsByCardSetId(int cardSetId, int pageIndex, int pageSize);
        void ShowGuid();
    }
}
