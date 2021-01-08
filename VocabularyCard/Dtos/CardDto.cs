using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }

        public string Vocabulary { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string Creator { get; set; }

        public string Modifier { get; set; }

        public CardState State { get; set; }

        public CardSetDto[] CardSets { get; set; }

        public CardInterpretationDto[] Interpretations { get; set; }

    }
}
