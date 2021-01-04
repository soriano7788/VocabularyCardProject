using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Domain
{
    public class CardInterpretation
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

        /// <summary>
        /// 詞性
        /// </summary>
        private PartOfSpeech _partOfSpeech;
        public PartOfSpeech PartOfSpeech
        {
            get { return _partOfSpeech; }
            set { _partOfSpeech = value; }
        }

        /// <summary>
        /// 音標
        /// </summary>
        private string _phoneticSymbol;
        public string PhoneticSymbol
        {
            get { return _phoneticSymbol; }
            set { _phoneticSymbol = value; }
        }

        /// <summary>
        /// 解釋
        /// </summary>
        private string _interpretation;
        public string Interpretation
        {
            get { return _interpretation; }
            set { _interpretation = value; }
        }


        // todo: 可拆解成 英文例句 和 例句(中文 or 其他語言，因為user並非只有中文使用者)翻譯???
        /// <summary>
        /// 例句
        /// </summary>
        private string _exampleSentence;
        public string ExampleSentence
        {
            get { return _exampleSentence; }
            set { _exampleSentence = value; }
        }

        private string _exampleSetenceExplanation;
        public string ExampleSentenceExplanation
        {
            get { return _exampleSetenceExplanation; }
            set { _exampleSetenceExplanation = value; }
        }

        private int _cardId;
        public int CardId
        {
            get { return _cardId; }
            set { _cardId = value; }
        }

    }
}
