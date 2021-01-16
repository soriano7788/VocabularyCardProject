using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.EF;
using VocabularyCard.Core.Repositories;
using VocabularyCard.Entities;

namespace VocabularyCard.Repositories.EF
{
    public class EFCardRepository : EFBaseRepository<Card>, ICardRepository
    {
        private IDbSet<Card> _cards;

        public EFCardRepository(IDbContext context) : base(context)
        {
            _cards = context.Set<Card>();
        }
        public Card GetByCardId(int cardId)
        {
            //return _cards.Find(cardId);

            // 使用 Include 的話，就會變成 eager loading
            // 參考 https://www.entityframeworktutorial.net/eager-loading-in-entity-framework.aspx
            return _cards.Where(c => c.CardId == cardId && c.State == CardState.Active).Include(c => c.Interpretations).First();
        }

        public IList<Card> GetByCardSetId(int cardSetId)
        {
            // many to many 關係，還不確定語法如何寫

            // todo: 或許我該考慮把 lazy loading 機制先關掉，
            // 避免利用存取運算子來 lazy loading 關聯資料
            // 而是需要 eager loading 時，查詢語法再加上 Include
            // 參考 http://vito-note.blogspot.com/2016/02/ef-loading.html

            // todo: 假如我想要同時，把 Card 的 Interpretation 抓出來，這樣的查詢夠嗎?
            // 是否應該讓 Card 和 CardInterpretation 永遠綁一起較好???
            IQueryable<Card> query = from c in _cards.Include(c => c.Interpretations)
                                     where c.CardSets.Any(cs => cs.CardSetId == cardSetId && cs.State == CardSetState.Active)
                                     select c;

            return query.ToList();
        }
    }
}
