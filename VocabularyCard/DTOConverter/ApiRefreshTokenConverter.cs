using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;
using VocabularyCard.DTO;

namespace VocabularyCard.DTOConverter
{
    public class ApiRefreshTokenConverter
    {
        public ApiRefreshToken ToDomainObject(ApiRefreshTokenInfo dto)
        {
            ApiRefreshToken domain = new ApiRefreshToken();
            domain.Vuid = dto.Vuid;
            domain.Token = dto.Token;
            domain.UserId = dto.UserId;
            domain.CreatedDateTime = dto.CreatedDateTime;
            domain.ExpiredDateTime = dto.ExpiredDateTime;
            return domain;
        }

        public ApiRefreshTokenInfo ToDataTransferObject(ApiRefreshToken domain)
        {
            ApiRefreshTokenInfo dto = new ApiRefreshTokenInfo();
            dto.Vuid = domain.Vuid;
            dto.Token = domain.Token;
            dto.UserId = domain.UserId;
            dto.CreatedDateTime = domain.CreatedDateTime;
            dto.ExpiredDateTime = domain.ExpiredDateTime;
            return dto;
        }

        public ApiRefreshToken[] ToDomainObjects(ApiRefreshTokenInfo[] dtos)
        {
            List<ApiRefreshToken> domains = new List<ApiRefreshToken>();
            foreach(ApiRefreshTokenInfo dto in dtos)
            {
                domains.Add(ToDomainObject(dto));
            }
            return domains.ToArray();
        }

        public ApiRefreshTokenInfo[] ToDataTransferObjects(ApiRefreshToken[] domains)
        {
            List<ApiRefreshTokenInfo> dtos = new List<ApiRefreshTokenInfo>();
            foreach (ApiRefreshToken domain in domains)
            {
                dtos.Add(ToDataTransferObject(domain));
            }
            return dtos.ToArray();
        }
    }
}
