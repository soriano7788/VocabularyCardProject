using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Services;
using VocabularyCard.Dtos;

namespace VocabularyCard.Services
{
    public interface ICardInterpretationService : IService
    {
        CardInterpretationDto[] GetInterpretationByCardId(int cardId);
    }
}
