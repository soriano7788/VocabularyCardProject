using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace VocabularyCard.Persistence.EF
{
    public class EFCardInterpretationDao : DbContextBase, ICardInterpretationDao
    {
        public EFCardInterpretationDao() : base() { }

        public IDbSet<CardInterpretation> CardInterpretations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var table = modelBuilder.Entity<CardInterpretation>().ToTable("CARD_INTERPRETATION");

            table.Property(c => c.Id)
                .IsRequired()
                .HasColumnName("ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            table.Property(c => c.Flag).IsRequired().HasColumnName("FLAG");

            // PartOfSpeech 的型態是自訂的 enum，此 enum 需繼承 byte
            // 參考 
            // https://stackoverflow.com/questions/50297148/entity-framework-core-2-0-mapping-enum-to-tinyint-in-sql-server-throws-exception
            table.Property(c => c.PartOfSpeech).IsRequired().HasColumnName("PART_OF_SPEECH").HasColumnType("tinyint");

            table.Property(c => c.PhoneticSymbol).IsRequired().HasColumnName("PHONETIC_SYMBOL").HasMaxLength(255);
            table.Property(c => c.Interpretation).IsRequired().HasColumnName("INTERPRETATION").HasMaxLength(255); ;
            table.Property(c => c.ExampleSentence).IsRequired().HasColumnName("EXAMPLE_SENTENCE");
            table.Property(c => c.ExampleSentenceExplanation).IsRequired().HasColumnName("EXAMPLE_SENTENCE_EXPLANATION");
            
            
            //table.Property(c => c.cardid).IsRequired().HasColumnName("CARD_ID");
        }


    }
}
