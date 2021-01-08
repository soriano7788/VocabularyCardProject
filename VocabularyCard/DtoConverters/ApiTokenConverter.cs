using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Entities;
using VocabularyCard.Dtos;

namespace VocabularyCard.DtoConverters
{
    public class ApiTokenConverter<TToken, TTokenDto> where TToken : BaseApiToken, new() where TTokenDto : BaseApiTokenDto, new()
    {
        public TToken ToEntity(TTokenDto dto)
        {
            TToken entity = new TToken();
            entity.Vuid = dto.Vuid;
            entity.Token = dto.Token;
            entity.UserId = dto.UserId;
            entity.CreatedDateTime = dto.CreatedDateTime;
            entity.ExpiredDateTime = dto.ExpiredDateTime;
            return entity;
        }

        public TTokenDto ToDataTransferObject(TToken entity)
        {
            TTokenDto dto = new TTokenDto();
            dto.Vuid = entity.Vuid;
            dto.Token = entity.Token;
            dto.UserId = entity.UserId;
            dto.CreatedDateTime = entity.CreatedDateTime;
            dto.ExpiredDateTime = entity.ExpiredDateTime;
            return dto;
        }

        public TToken[] ToEntities(TTokenDto[] dtos)
        {
            List<TToken> entities = new List<TToken>();
            foreach(TTokenDto dto in dtos)
            {
                entities.Add(ToEntity(dto));
            }
            return entities.ToArray();
        }
        public TTokenDto[] ToDataTransferObjects(TToken[] entities)
        {
            List<TTokenDto> dtos = new List<TTokenDto>();
            foreach (TToken entity in entities)
            {
                dtos.Add(ToDataTransferObject(entity));
            }
            return dtos.ToArray();
        }

    }
}
