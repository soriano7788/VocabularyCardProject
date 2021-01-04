using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;
using VocabularyCard.Util;
using VocabularyCard.Persistence.EF.Mapping;

namespace VocabularyCard.Persistence.EF
{
    public class EFCardSetDao : DbContextBase, ICardSetDao
    {
        private CardSetMap _cardSetMap;
        private CardMap _cardMap;
        //public EFCardSetDao() : base("vocabulary") { }
        public EFCardSetDao() : base()
        {
            // base 內的參數是 web.config 裡設定的 connectionString 的 name
            // 所以我應該再建一個 DbContextBase，裡面先做好 connectionString 設定?
            // 不然每個地方都要注一次很麻煩
            // 之前 KM 每個 DAO 實作 是有一個 GetSession method，裡面可能有做類似的事??

            // todo: 讓 DbContextBase 的無參數建構式，裡面設定好 connectionString 就好，是否可行??
        }

        public EFCardSetDao(CardSetMap cardSetMap, CardMap cardMap) : base()
        {
            _cardSetMap = cardSetMap;
            _cardMap = cardMap;
        }

        public IDbSet<CardSet> CardSets { get; set; }

        // todo: 這個 OnModelCreating method 只有初始化的時候會呼叫嗎??? 印 LOG 測試看看
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //this.Database.Connection.ConnectionString = "";
            modelBuilder.Configurations.Add(_cardSetMap);
            modelBuilder.Configurations.Add(_cardMap);
            // 防止在 database 內建立的 table 命名被加上複數，e.g. CARDS
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<CardSet>()
            //    .HasMany(cs => cs.Cards)
            //    .WithMany(c => c.CardSets)
            //    .Map(m =>
            //    {
            //        m.ToTable("CARD_SET_CARD_EDGE");
            //        m.MapLeftKey("CARD_SET_ID");
            //        m.MapRightKey("CARD_ID");
            //    });
            //modelBuilder.Configurations.Add

            //var cardSetTable = modelBuilder.Entity<CardSet>().ToTable("CARD_SET");

            // todo: 大問題，看起來下面的 table property map 被無視掉???
            // 上面問題發現原因了，card 的 map 沒寫在這邊的，這邊會不知道!!!
            // 幹!!!搞屁，這樣很麻煩，沒理由這邊要再設定一次 card 的 mapping 阿?
            // 難不成要合併 CardSet 和 Card dao??? 然後實作再各自繼承???
            // 總覺得詭異....



            // 在不行的話就先試 one-to-many
            // 再試不出來的話就只能先用 linq 或 sql 自行 join 了......

            // 還有一種方式，就是 CardSet 和 Card 裡面的 property 就直接設 CardSetCardEdge 了
            // 只是觀看上好像怪怪的?
            // 但是官方文件也確實有這種寫法

            #region 舊的 cardSet map 設定
            // todo: 突然發現下面設定完全沒有用到 CardSetCardEdge class
            //cardSetTable
            //    .HasMany(cs => cs.Cards)
            //    .WithMany(c => c.CardSets)
            //    .Map(m =>
            //    {
            //        m.ToTable("CARD_SET_CARD_EDGE");
            //        m.MapLeftKey("CARD_SET_ID");
            //        m.MapRightKey("CARD_ID");
            //    });

            //cardSetTable.Property(c => c.CardSetId)
            //    .IsRequired()
            //    .HasColumnName("CARD_SET_ID")
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //cardSetTable.Property(c => c.Flag).IsRequired().HasColumnName("FLAG");
            //cardSetTable.Property(c => c.DisplayName).IsRequired().HasColumnName("DISPLAYNAME").HasMaxLength(255);
            //cardSetTable.Property(c => c.Description).IsRequired().HasColumnName("DESCRIPTION");
            //cardSetTable.Property(c => c.CreatedDateTime).IsRequired().HasColumnName("CREATED_DATETIME");
            //cardSetTable.Property(c => c.ModifiedDateTime).IsRequired().HasColumnName("MODIFIED_DATETIME");
            //cardSetTable.Property(c => c.Creator).IsRequired().HasColumnName("CREATOR").HasMaxLength(255);
            //cardSetTable.Property(c => c.Modifier).IsRequired().HasColumnName("MODIFIER").HasMaxLength(255);
            //cardSetTable.Property(c => c.Owner).IsRequired().HasColumnName("OWNER").HasMaxLength(255);
            //// State 的型態是自訂的 enum，此 enum 需繼承 byte
            //// 參考 
            //// https://stackoverflow.com/questions/50297148/entity-framework-core-2-0-mapping-enum-to-tinyint-in-sql-server-throws-exception
            //cardSetTable.Property(c => c.State).IsRequired().HasColumnName("STATE").HasColumnType("tinyint");


            // 設定 CardSet to Card 的 many to many，參考下面連結
            // https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration


            #endregion

            #region 舊的 card map 設定

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

            base.OnModelCreating(modelBuilder);
        }
        public CardSet Get(int id)
        {
            CardSet cardSet = CardSets.Find(id);
            // 假如 id 搜尋結果不存在的話，card 會是 null

            return cardSet;
        }
        public CardSet[] GetAll()
        {
            // 要再確認此寫法是否可行
            //List<CardSet> cardSets = CardSets.ToList();
            //return cardSets.ToArray();

            //LogUtility.ErrorLog("EF Database.Connection.ConnectionString: " + Database.Connection.ConnectionString);

            var q = from row in CardSets select row;
            return q.ToArray();


            // 寫原生 SQL 的例子，需要查詢參數的話，SqlQuery method 可以加，和 String.Format 格式類似
            //var querySql = this.Database.SqlQuery<CardSet>("SELECT * FROM CARD_SET");
            //return querySql.ToArray();

        }
        public CardSet[] GetByOwner(string ownerId)
        {
            IQueryable<CardSet> results = CardSets.Where(cs => cs.Owner == ownerId);
            return results.ToArray();
        }

        public CardSet Create(CardSet cardSet)
        {
            // entity framework 處理 concurrency exception，有一些處理手法，有空要再搜尋看一下
            CardSet newCardSet = CardSets.Add(cardSet);
            SaveChanges();
            return newCardSet;
        }

        public CardSet Update(CardSet cardSet)
        {
            // 這裡主動改狀態，主動執行 save(commit 的意思??)，寫法是否 OK??
            Entry(cardSet).State = EntityState.Modified;
            SaveChanges();

            return cardSet;
        }

        //public Card[] GetCardsByCardSetId(int cardSetId)
        //{
            
        //}
    }
}
