using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Domain
{
    [Table("CARD_SET")]
    public class CardSet : BaseEntity
    {
        private int _cardSetid;
        public int CardSetId
        {
            get { return _cardSetid; }
            set { _cardSetid = value; }
        }

        private int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
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

        private string _owner;
        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        private CardSetState _state;
        public CardSetState State
        {
            get { return _state; }
            set { _state = value; }
        }

        private ICollection<Card> _cards;
        /// <summary>
        /// Navigation properties are typically defined as virtual 
        /// so that they can take advantage of certain Entity Framework functionality 
        /// such as lazy loading. (Lazy loading will be explained later, 
        /// in the Reading Related Data tutorial later in this series.)
        /// </summary>
        public virtual ICollection<Card> Cards
        {
            get { return _cards; }
            set { _cards = value; }
        }


    }
}
