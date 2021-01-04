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
    public class CardInterpretationMap : EntityTypeConfiguration<CardInterpretation>
    {
        public CardInterpretationMap()
        {
            this.ToTable("CARD_INTERPRETATION");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(c => c.Flag).IsRequired().HasColumnName("FLAG");
            this.Property(c => c.PartOfSpeech).IsRequired().HasColumnName("PART_OF_SPEECH").HasColumnType("tinyint");
            this.Property(c => c.PhoneticSymbol).IsRequired().HasColumnName("PHONETIC_SYMBOL").HasMaxLength(255);
            this.Property(c => c.ExampleSentence).IsRequired().HasColumnName("EXAMPLE_SENTENCE");
            this.Property(c => c.ExampleSentenceExplanation).IsRequired().HasColumnName("EXAMPLE_SENTENCE_EXPLANATION");
            this.Property(c => c.CardId).IsRequired().HasColumnName("CARD_ID");



        }
    }
}
