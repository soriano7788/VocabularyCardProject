using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;
using VocabularyCard.DTO;

namespace VocabularyCard.DTOConverter
{
    public class ApiAccessTokenConverter
    {
        public ApiAccessToken ToDomainObject(ApiAccessTokenInfo dto)
        {
            ApiAccessToken domain = new ApiAccessToken();
            domain.Vuid = dto.Vuid;
            domain.Token = dto.Token;
            domain.UserId = dto.UserId;
            domain.CreatedDateTime = dto.CreatedDateTime;
            domain.ExpiredDateTime = dto.ExpiredDateTime;
            return domain;
        }

        public ApiAccessTokenInfo ToDataTransferObject(ApiAccessToken domain)
        {
            ApiAccessTokenInfo dto = new ApiAccessTokenInfo();
            dto.Vuid = domain.Vuid;
            dto.Token = domain.Token;
            dto.UserId = domain.UserId;
            dto.CreatedDateTime = domain.CreatedDateTime;
            dto.ExpiredDateTime = domain.ExpiredDateTime;
            return dto;
        }

        public ApiAccessToken[] ToDomainObjects(ApiAccessTokenInfo[] dtos)
        {
            List<ApiAccessToken> domains = new List<ApiAccessToken>();
            foreach (ApiAccessTokenInfo dto in dtos)
            {
                domains.Add(ToDomainObject(dto));
            }
            return domains.ToArray();
        }

        public ApiAccessTokenInfo[] ToDataTransferObjects(ApiAccessToken[] domains)
        {
            List<ApiAccessTokenInfo> dtos = new List<ApiAccessTokenInfo>();
            foreach (ApiAccessToken domain in domains)
            {
                dtos.Add(ToDataTransferObject(domain));
            }
            return dtos.ToArray();
        }
    }
}
