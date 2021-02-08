using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Entities;
using VocabularyCard.Dtos;

namespace VocabularyCard.DtoConverters
{
    public class CardInterpretationConverter
    {
        public CardInterpretation ToEntity(CardInterpretationDto dto)
        {
            CardInterpretation entity = new CardInterpretation();
            entity.Id = dto.Id;
            entity.PartOfSpeech = dto.PartOfSpeech;
            entity.PhoneticSymbol = dto.PhoneticSymbol;
            entity.Interpretation = dto.Interpretation;
            entity.ExampleSentence = dto.ExampleSentence;
            entity.ExampleSentenceExplanation = dto.ExampleSentenceExplanation;
            entity.State = dto.State;
            entity.CardId = dto.CardId;

            return entity;
        }
        public CardInterpretationDto ToDataTransferObject(CardInterpretation entity) 
        {
            CardInterpretationDto dto = new CardInterpretationDto();
            dto.Id = entity.Id;
            dto.PartOfSpeech = entity.PartOfSpeech;
            dto.PhoneticSymbol = entity.PhoneticSymbol;
            dto.Interpretation = entity.Interpretation;
            dto.ExampleSentence = entity.ExampleSentence;
            dto.ExampleSentenceExplanation = entity.ExampleSentenceExplanation;
            dto.State = entity.State;
            dto.CardId = entity.CardId;

            return dto;
        }

        public CardInterpretation[] ToEntities(CardInterpretationDto[] dtos)
        {
            List<CardInterpretation> entities = new List<CardInterpretation>();
            foreach(CardInterpretationDto dto in dtos)
            {
                entities.Add(ToEntity(dto));
            }
            return entities.ToArray();
        }
        public CardInterpretationDto[] ToDataTransferObjects(CardInterpretation[] entities)
        {
            List<CardInterpretationDto> dtos = new List<CardInterpretationDto>();
            foreach (CardInterpretation entity in entities)
            {
                dtos.Add(ToDataTransferObject(entity));
            }
            return dtos.ToArray();
        }
    }
}
