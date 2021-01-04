using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using VocabularyCard.Persistence.EF.Mapping;

namespace VocabularyCard.Persistence.EF
{
    public class EFCardDao : DbContextBase, ICardDao
    {
        private CardMap _cardMap;
        //public EFCardDao() : base("vocabulary") 
        public EFCardDao() : base()
        {
            // base 內的參數是 web.config 裡設定的 connectionString 的 name
            // 所以我應該再建一個 DbContextBase，裡面先做好 connectionString 設定?
            // 不然每個地方都要注一次很麻煩
            // 之前 KM 每個 DAO 實作 是有一個 GetSession method，裡面可能有做類似的事??

            // todo: 讓 DbContextBase 的無參數建構式，裡面設定好 connectionString 就好，是否可行??
        }

        public EFCardDao(CardMap cardMap) : base()
        {
            _cardMap = cardMap;
        }

        //public DbSet<Card> Cards { get; set; }
        // 利用程式碼 來產生 db 的 schema 的話，會以此欄位名稱來命名
        public IDbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 防止在 database 內建立的 table 命名被加上複數，e.g. CARDS
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Card>()
            //    .HasMany(c => c.CardSets)
            //    .WithMany(cs => cs.Cards)
            //    .Map(m =>
            //    {
            //        m.ToTable("CARD_SET_CARD_EDGE");
            //        m.MapLeftKey("CARD_SET_ID");
            //        m.MapRightKey("CARD_ID");
            //    });

            #region 這是舊的 mapping 寫法設定

            //// 這是c# class 和 db table 的對應設定
            //var cardTable = modelBuilder.Entity<Card>().ToTable("CARD");

            //cardTable
            //    .HasMany(c => c.CardSets)
            //    .WithMany(cs => cs.Cards)
            //    .Map(m =>
            //    {
            //        m.ToTable("CARD_SET_CARD_EDGE");
            //        m.MapLeftKey("CARD_SET_ID");
            //        m.MapRightKey("CARD_ID");
            //    });

            //cardTable.Property(c => c.CardId)
            //    .IsRequired()
            //    .HasColumnName("CARD_ID")
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //cardTable.Property(c => c.Flag).IsRequired().HasColumnName("FLAG");
            //cardTable.Property(c => c.Vocabulary).IsRequired().HasColumnName("VOCABULARY").HasMaxLength(255);
            //cardTable.Property(c => c.CreatedDateTime).IsRequired().HasColumnName("CREATED_DATETIME");
            //cardTable.Property(c => c.ModifiedDateTime).IsRequired().HasColumnName("MODIFIED_DATETIME");
            //cardTable.Property(c => c.Creator).IsRequired().HasColumnName("CREATOR").HasMaxLength(255);
            //cardTable.Property(c => c.Modifier).IsRequired().HasColumnName("MODIFIER").HasMaxLength(255);
            //// State 的型態是自訂的 enum，此 enum 需繼承 byte
            //// 參考 
            //// https://stackoverflow.com/questions/50297148/entity-framework-core-2-0-mapping-enum-to-tinyint-in-sql-server-throws-exception
            //cardTable.Property(c => c.State).IsRequired().HasColumnName("STATE").HasColumnType("tinyint");

            #endregion

            modelBuilder.Configurations.Add(_cardMap);
            base.OnModelCreating(modelBuilder);
        }


        public Card Get(int id)
        {
            Card card = Cards.Find(id);
            // 假如 id 搜尋結果不存在的話，card 會是 null

            return card;
        }

        //public Card[] GetByCardSetId(int cardSetId)
        //{
        //    // 需要 join CardSetCardEdge 和 Card


        //    /* 這個 sql 會把 edge 的 欄位也接在後面撈出來
        //    select * 
        //    from CARD as c join CARD_CARD_SET_EDGE as edge 
        //    on c.ID = edge.CARD_ID 
        //    where edge.CARD_SET_ID =1;
        //    */
        //}


        public Card Create(Card card)
        {
            Card newCard = Cards.Add(card);
            SaveChanges();
            
            return newCard;
        }
    }
}
