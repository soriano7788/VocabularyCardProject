using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Persistence
{
    public interface ICardDao
    {
        Card Get(int id);
        Card Create(Card card);
    }
}
