using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Repositories;
using VocabularyCard.Entities;

namespace VocabularyCard.Repositories
{
    public interface ICardSetRepository : IRepository<CardSet>
    {
        CardSet GetByCardSetId(int cardSetId);
        IList<CardSet> GetByOwner(string ownerId);
    }
}
