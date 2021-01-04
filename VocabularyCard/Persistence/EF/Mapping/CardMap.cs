using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VocabularyCard.Domain;

namespace VocabularyCard.Persistence.EF.Mapping
{
    public class CardMap : EntityTypeConfiguration<Card>
    {
        public CardMap()
        {
            this.ToTable("CARD");
            this.HasKey(t => t.CardId);
            this.Property(t => t.CardId)
                .IsRequired()
                .HasColumnName("CARD_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(c => c.Flag).IsRequired().HasColumnName("FLAG");
            this.Property(c => c.Vocabulary).IsRequired().HasColumnName("VOCABULARY").HasMaxLength(255);
            this.Property(c => c.CreatedDateTime).IsRequired().HasColumnName("CREATED_DATETIME");
            this.Property(c => c.ModifiedDateTime).IsRequired().HasColumnName("MODIFIED_DATETIME");
            this.Property(c => c.Creator).IsRequired().HasColumnName("CREATOR").HasMaxLength(255);
            this.Property(c => c.Modifier).IsRequired().HasColumnName("MODIFIER").HasMaxLength(255);
            // State 的型態是自訂的 enum，此 enum 需繼承 byte
            // 參考 
            // https://stackoverflow.com/questions/50297148/entity-framework-core-2-0-mapping-enum-to-tinyint-in-sql-server-throws-exception
            this.Property(c => c.State).IsRequired().HasColumnName("STATE").HasColumnType("tinyint");

            this
                .HasMany(c => c.CardSets)
                .WithMany(cs => cs.Cards)
                .Map(m =>
                {
                    m.ToTable("CARD_SET_CARD_EDGE");
                    m.MapLeftKey("CARD_SET_ID");
                    m.MapRightKey("CARD_ID");
                });
        }
    }
}
