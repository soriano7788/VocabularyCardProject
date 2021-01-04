using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Domain
{
    [Table("CARD")]
    public class Card : BaseEntity
    {
        private int _cardId;
        [Key]
        public int CardId
        {
            get { return _cardId; }
            set { _cardId = value; }
        }

        private int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        private string _vocabulary;
        [Required, MaxLength(100)]
        public string Vocabulary
        {
            get { return _vocabulary; }
            set { _vocabulary = value; }
        }

        private DateTime _createdDateTime;
        public DateTime CreatedDateTime
        {
            get { return _createdDateTime; }
            set { _createdDateTime = value; }
        }

        private DateTime _modifiedDateTime;
        public DateTime ModifiedDateTime
        {
            get { return _modifiedDateTime; }
            set { _modifiedDateTime = value; }
        }

        private string _creator;
        public string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        private string _modifier;
        public string Modifier
        {
            get { return _modifier; }
            set { _modifier = value; }
        }

        private CardState _state;
        public CardState State
        {
            get { return _state; }
            set { _state = value; }
        }

        private ICollection<CardInterpretation> _interpretations;
        public ICollection<CardInterpretation> Interpretations
        {
            get { return _interpretations; }
            set { _interpretations = value; }
        }

        private ICollection<CardSet> _cardSets;
        public virtual ICollection<CardSet> CardSets
        {
            get { return _cardSets; }
            set { _cardSets = value; }
        }

    }
}
