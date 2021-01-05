using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Repositories;
using VocabularyCard.Entities;

namespace VocabularyCard.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        IList<Card> GetByCardSetId(int cardSetId);
    }
}
