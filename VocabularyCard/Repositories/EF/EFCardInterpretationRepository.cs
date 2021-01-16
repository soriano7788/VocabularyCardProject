using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Repositories;
using VocabularyCard.Core.EF;
using VocabularyCard.Entities;
using System.Data.Entity;

namespace VocabularyCard.Repositories.EF
{
    public class EFCardInterpretationRepository : EFBaseRepository<CardInterpretation>, ICardInterpretationRepository
    {
        private IDbSet<CardInterpretation> _interpretations;
        public EFCardInterpretationRepository(IDbContext context) : base(context)
        {
            _interpretations = context.Set<CardInterpretation>();
        }

        public IList<CardInterpretation> GetByCardId(int cardId)
        {
            return _interpretations.Where(ci => ci.CardId == cardId).ToList();
        }

    }
}
