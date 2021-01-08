using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Entities;

namespace VocabularyCard.Entities
{
    public class Card : BaseEntity
    {
        public int CardId { get; set; }
        public string Vocabulary { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public CardState State { get; set; }
        public virtual ICollection<CardInterpretation> Interpretations { get; set; }
        public ICollection<CardSet> CardSets { get; set; }
    }
}
