using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Test.Repository.EF
{
    public class EFCardRepository : EFBaseRepository<Card>, ICardRepository
    {
        private readonly IDbSet<Card> _cards;

        public EFCardRepository(DbContext context) : base(context) 
        {
            _cards = context.Set<Card>();
        }
    }
}
