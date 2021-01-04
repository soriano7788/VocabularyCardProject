using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Persistence.EF
{
    public class EFCardSetCardEdgeDao : DbContextBase, ICardSetCardDao
    {
        public EFCardSetCardEdgeDao() : base()
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CardSetCardEdge>().ToTable("CARD_SET_CARD_EDGE");
            //modelBuilder.Entity<CardSetCardEdge>()
            //    .HasKey(edge => new { edge.CardSetId, edge.CardId });

            //modelBuilder.Entity<CardSet>()
            //    .HasMany(cs => cs.Cards)
            //    .WithMany(e => e.CardSets)
            //    .Map(m => {
            //        m.MapLeftKey("ID");
            //        m.MapRightKey("ID");
            //        m.ToTable("CARD_SET_CARD_EDGE");
            //    });

            //modelBuilder.Entity<CardSet>()
            //    .HasMany(cs => cs.Cards)
            //    .WithMany(c => c.CardSets)
            //    .Map(m => {
            //        m.ToTable("CARD_SET_CARD_EDGE");
            //        m.MapLeftKey("CARD_SET_ID");
            //        m.MapRightKey("CARD_ID");
            //    });

            //modelBuilder.Entity<Card>()
            //    .HasMany(c => c.CardSets)
            //    .WithMany(cs => cs.Cards)
            //    .Map(m => {
            //        m.ToTable("CARD_SET_CARD_EDGE");
            //        m.MapLeftKey("CARD_SET_ID");
            //        m.MapRightKey("CARD_ID");
            //    });

            //base.OnModelCreating(modelBuilder);
        }
        public IDbSet<CardSet> CardSets { get; set; }
        public IDbSet<Card> Cards { get; set; }
    }
}
