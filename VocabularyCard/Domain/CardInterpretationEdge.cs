using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Domain
{
    public class CardInterpretationEdge
    {
        private int _cardId;
        public int CardId
        {
            get { return _cardId; }
            set { _cardId = value; }
        }

        private int _cardInterpretationId;
        public int CardInterpretationId
        {
            get { return _cardInterpretationId; }
            set { _cardInterpretationId = value; }
        }
    }
}
