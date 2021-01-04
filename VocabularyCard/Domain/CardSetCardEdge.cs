using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Domain
{
    [Table("CARD_SET_CARD_EDGE")]
    public class CardSetCardEdge
    {
        private int _cardSetId;
        public int CardSetId
        {
            get { return _cardSetId; }
            set { _cardSetId = value; }
        }

        private int _cardId;
        public int CardId
        {
            get { return _cardId; }
            set { _cardId = value; }
        }


    }
}
