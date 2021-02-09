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

        public void CreateInterpretations(CardInterpretation[] interpretations)
        {
            (_interpretations as DbSet<CardInterpretation>).AddRange(interpretations);
        }

        public void UpdateMany(CardInterpretation[] interpretations)
        {
            // 挑戰只更新五個欄位，不要去更新 STATE 欄位做多餘的事
            //_interpretations.

            Dictionary<int, CardInterpretation> dic = interpretations.ToDictionary(interpret => interpret.Id, interpret => interpret);
            int[] ids = interpretations.Select(interpret => interpret.Id).ToArray();
            var query = _interpretations.Where(interpret => ids.Contains(interpret.Id)).ToList();
            query.ForEach(interpret => 
            {
                interpret.PartOfSpeech = dic[interpret.Id].PartOfSpeech;
                interpret.PhoneticSymbol = dic[interpret.Id].PhoneticSymbol;
                interpret.Interpretation = dic[interpret.Id].Interpretation;
                interpret.ExampleSentence = dic[interpret.Id].ExampleSentence;
                interpret.ExampleSentenceExplanation = dic[interpret.Id].ExampleSentenceExplanation;
            });
        }

        public void RemoveMany(int[] interpretationIds)
        {
            // Remove 獨立出來是為了只修改 STATE 這個 column 為 Removed，
            // 假如直接用 Update 會變成複寫多個 column
            var interprets = _interpretations.Where(interpret => interpretationIds.Contains(interpret.Id)).ToList();
            interprets.ForEach(i => i.State = CardInterpretationState.Removed);
        }

    }
}
