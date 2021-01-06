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

        public EFCardRepository(DbContext context) : base(context)
        {
            _cards = context.Set<Card>();
        }
        public Card GetByCardId(int cardId)
        {
            return _cards.Find(cardId);
        }
        public IList<Card> GetByCardSetId(int cardSetId)
        {
            throw new NotImplementedException();
        }
    }
}
