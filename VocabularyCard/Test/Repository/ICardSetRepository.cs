using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Test.Repository
{
    public interface ICardSetRepository : IRepository<CardSet>
    {
        List<CardSet> GetByOwner(string ownerId);
    }
}
