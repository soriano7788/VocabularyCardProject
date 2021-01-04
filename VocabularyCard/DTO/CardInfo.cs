using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.DTO
{
    public class CardInfo
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        private string _vocabulary;
        public string Vocabulary
        {
            get { return _vocabulary; }
            set { _vocabulary = value; }
        }

        private DateTime _createdDatetime;
        public DateTime CreatedDateTime
        {
            get { return _createdDatetime; }
            set { _createdDatetime = value; }
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

        private CardSetInfo[] _cardSetInfos;
        public CardSetInfo[] CardSetInfos
        {
            get { return _cardSetInfos; }
            set { _cardSetInfos = value; }
        }

    }
}
