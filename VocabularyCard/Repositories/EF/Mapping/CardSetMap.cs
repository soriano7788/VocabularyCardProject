﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.EF;
using VocabularyCard.Entities;

namespace VocabularyCard.Repositories.EF.Mapping
{
    public class CardSetMap : EntityTypeConfiguration<CardSet>, IEntityTypeConfiguration
    {
        public CardSetMap()
        {
            this.ToTable("CARD_SET");
            this.HasKey(t => t.CardSetId);
            this.Property(t => t.CardSetId)
                .IsRequired()
                .HasColumnName("CARD_SET_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(c => c.DisplayName).IsRequired().HasColumnName("DISPLAYNAME").HasMaxLength(255);
            this.Property(c => c.Description).HasColumnName("DESCRIPTION");
            this.Property(c => c.CreatedDateTime).IsRequired().HasColumnName("CREATED_DATETIME");
            this.Property(c => c.ModifiedDateTime).IsRequired().HasColumnName("MODIFIED_DATETIME");
            this.Property(c => c.Creator).IsRequired().HasColumnName("CREATOR").HasMaxLength(255);
            this.Property(c => c.Modifier).IsRequired().HasColumnName("MODIFIER").HasMaxLength(255);
            this.Property(c => c.Owner).IsRequired().HasColumnName("OWNER").HasMaxLength(255);
            // State 的型態是自訂的 enum，此 enum 需繼承 byte
            // 參考 
            // https://stackoverflow.com/questions/50297148/entity-framework-core-2-0-mapping-enum-to-tinyint-in-sql-server-throws-exception
            this.Property(c => c.State).IsRequired().HasColumnName("STATE").HasColumnType("tinyint");

            this
                .HasMany(cs => cs.Cards)
                .WithMany(c => c.CardSets)
                .Map(
                    m =>
                    {
                        m.ToTable("CARD_SET_CARD_EDGE");
                        m.MapLeftKey("CARD_SET_ID");
                        m.MapRightKey("CARD_ID");
                    }
                );
        }
    }
}
