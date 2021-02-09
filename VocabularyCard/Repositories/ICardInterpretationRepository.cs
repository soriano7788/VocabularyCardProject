using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Repositories;
using VocabularyCard.Entities;

namespace VocabularyCard.Repositories
{
    public interface ICardInterpretationRepository : IRepository<CardInterpretation>
    {
        IList<CardInterpretation> GetByCardId(int cardId);

        void CreateInterpretations(CardInterpretation[] interpretations);

        void UpdateMany(CardInterpretation[] interpretations);

        void RemoveMany(int[] interpretationIds);
    }
}
