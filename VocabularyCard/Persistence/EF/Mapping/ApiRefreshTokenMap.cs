using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Persistence.EF.Mapping
{
    public class ApiRefreshTokenMap: EntityTypeConfiguration<ApiRefreshToken>
    {
        public ApiRefreshTokenMap()
        {
            ToTable("API_REFRESH_TOKEN");
            HasKey(t => t.Vuid);
            Property(t => t.Vuid)
                .IsRequired()
                .HasColumnName("VUID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Token).IsRequired().HasColumnName("TOKEN").HasMaxLength(255);
            Property(t => t.UserId).IsRequired().HasColumnName("USER_ID").HasMaxLength(255);
            Property(t => t.ExpiredDateTime).IsRequired().HasColumnName("EXPIRED_DATETIME");
            Property(t => t.CreatedDateTime).IsRequired().HasColumnName("CREATED_DATETIME");
        }
    }
}
