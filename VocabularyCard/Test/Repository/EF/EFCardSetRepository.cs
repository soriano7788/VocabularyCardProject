using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Test.Repository.EF
{
    public class EFCardSetRepository : EFBaseRepository<CardSet>, ICardSetRepository
    {
        private  DbContext _context;
        private readonly IDbSet<CardSet> _cardSets;

        public EFCardSetRepository(DbContext context) : base(context) 
        {
            //_context = context;
            _cardSets = context.Set<CardSet>();
        }

        public List<CardSet> GetByOwner(string ownerId)
        {
            var cardSets = _cardSets.Where(cs => cs.Owner == ownerId);
            return cardSets.ToList();
        }
    }
}
