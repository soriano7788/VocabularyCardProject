using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.EF;
using VocabularyCard.Entities;

namespace VocabularyCard.Repositories.EF
{
    public class EFCardSetRepository : EFBaseRepository<CardSet>, ICardSetRepository
    {
        private DbContext _context;
        private readonly IDbSet<CardSet> _cardSets;

        public EFCardSetRepository(DbContext context) : base(context)
        {
            _context = context;
            _cardSets = context.Set<CardSet>();
        }
        public CardSet GetByCardSetId(int cardSetId)
        {
            return _cardSets.Where(cs => cs.CardSetId == cardSetId && cs.State == CardSetState.Active).First();
        }
        public IList<CardSet> GetByOwner(string ownerId)
        {
            IQueryable<CardSet> cardSets = _cardSets.Where(cs => cs.Owner == ownerId && cs.State == CardSetState.Active);
            return cardSets.ToList();
        }

        public override CardSet Update(CardSet cardset)
        {
            CardSet entity = _cardSets.Find(cardset.CardSetId);
            _context.Entry(entity).CurrentValues.SetValues(cardset);
            return cardset;
        }
    }
}
