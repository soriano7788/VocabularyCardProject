using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Persistence
{
    public interface ICardSetDao
    {
        CardSet Get(int id);
        CardSet[] GetAll();
        CardSet[] GetByOwner(string ownerId);
        CardSet Create(CardSet cardSet);
    }
}
