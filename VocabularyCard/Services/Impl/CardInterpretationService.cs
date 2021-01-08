using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Services;
using VocabularyCard.Core;
using VocabularyCard.Repositories;
using VocabularyCard.Entities;
using VocabularyCard.Dtos;
using VocabularyCard.DtoConverters;

namespace VocabularyCard.Services.Impl
{
    public class CardInterpretationService : BaseService, ICardInterpretationService
    {
        private ICardInterpretationRepository _cardInterpretationRepository;
        private CardInterpretationConverter _cardInterpretationConverter;

        public CardInterpretationService(IUnitOfWork unitOfWork, ICardInterpretationRepository cardInterpretationRepository) :base(unitOfWork)
        {
            _cardInterpretationRepository = cardInterpretationRepository;
            _cardInterpretationConverter = new CardInterpretationConverter();
        }

        public CardInterpretationDto[] GetInterpretationByCardId(int cardId)
        {
            IList<CardInterpretation> interpretations = _cardInterpretationRepository.GetByCardId(cardId);
            return _cardInterpretationConverter.ToDataTransferObjects(interpretations.ToArray());
        }
    }
}
