using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Entities;

namespace VocabularyCard.Entities
{
    public class CardInterpretation : BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 詞性
        /// </summary>
        public PartOfSpeech PartOfSpeech { get; set; }

        /// <summary>
        /// 音標
        /// </summary>
        public string PhoneticSymbol { get; set; }

        /// <summary>
        /// 解釋
        /// </summary>
        public string Interpretation { get; set; }

        // todo: 可拆解成 英文例句 和 例句(中文 or 其他語言，因為user並非只有中文使用者)翻譯???
        /// <summary>
        /// 例句
        /// </summary>
        public string ExampleSentence { get; set; }

        public string ExampleSentenceExplanation { get; set; }

        public int CardId { get; set; }
    }
}
