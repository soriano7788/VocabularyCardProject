using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Entities;
using VocabularyCard.Dtos;

namespace VocabularyCard.DtoConverters
{
    public class CardSetConverter
    {
        public CardSet ToEntity(CardSetDto dto)
        {
            CardSet cardSet = new CardSet();
            cardSet.CardSetId = dto.Id;
            cardSet.DisplayName = dto.DisplayName;
            cardSet.Description = dto.Description;
            cardSet.Creator = dto.Creator;
            cardSet.Modifier = dto.Modifier;
            cardSet.Owner = dto.Owner;
            cardSet.CreatedDateTime = dto.CreatedDateTime;
            cardSet.ModifiedDateTime = dto.ModifiedDateTime;
            cardSet.State = dto.State;

            return cardSet;
        }
        public CardSet[] ToEntities(CardSetDto[] dtos)
        {
            List<CardSet> dto = new List<CardSet>();
            foreach (CardSetDto cardSetInfo in dtos)
            {
                dto.Add(ToEntity(cardSetInfo));
            }

            return dto.ToArray();
        }

        public CardSetDto ToDataTransferObject(CardSet entity)
        {
            CardSetDto cardSetDto = new CardSetDto();
            cardSetDto.Id = entity.CardSetId;
            cardSetDto.DisplayName = entity.DisplayName;
            cardSetDto.Description = entity.Description;
            cardSetDto.Creator = entity.Creator;
            cardSetDto.Modifier = entity.Modifier;
            cardSetDto.Owner = entity.Owner;
            cardSetDto.CreatedDateTime = entity.CreatedDateTime;
            cardSetDto.ModifiedDateTime = entity.ModifiedDateTime;
            cardSetDto.State = entity.State;

            return cardSetDto;
        }
        public CardSetDto[] ToDataTransferObjects(CardSet[] entities)
        {
            List<CardSetDto> cardSetDtos = new List<CardSetDto>();
            foreach (CardSet entity in entities)
            {
                cardSetDtos.Add(ToDataTransferObject(entity));
            }

            return cardSetDtos.ToArray();
        }
    }
}
