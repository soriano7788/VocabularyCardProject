using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Test.Repository
{
    public interface ICardRepository : IRepository<Card>
    {
    }
}
