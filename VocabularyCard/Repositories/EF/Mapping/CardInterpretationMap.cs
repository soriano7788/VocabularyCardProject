using System;
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
    public class CardInterpretationMap : EntityTypeConfiguration<CardInterpretation>, IEntityTypeConfiguration
    {
        public CardInterpretationMap()
        {
            this.ToTable("CARD_INTERPRETATION");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(c => c.PartOfSpeech).IsRequired().HasColumnName("PART_OF_SPEECH").HasColumnType("tinyint");
            this.Property(c => c.PhoneticSymbol).IsRequired().HasColumnName("PHONETIC_SYMBOL").HasMaxLength(255);
            this.Property(c => c.ExampleSentence).IsRequired().HasColumnName("EXAMPLE_SENTENCE");
            this.Property(c => c.ExampleSentenceExplanation).IsRequired().HasColumnName("EXAMPLE_SENTENCE_EXPLANATION");
            this.Property(c => c.CardId).IsRequired().HasColumnName("CARD_ID");

            //this.HasRequired<Card>(ci => ci.Card)
            //    .WithMany(c => c.Interpretations)
            //    .HasForeignKey<int>(ci => ci.CardId);
        }
    }
}
