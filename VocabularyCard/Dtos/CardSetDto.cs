using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Dtos
{
    public class CardSetDto
    {
        public int Id { get; set; }

        public int Flag { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string Creator { get; set; }

        public string Modifier { get; set; }

        public string Owner { get; set; }

        public CardSetState State { get; set; }

        public CardDto[] Cards { get; set; }
    }
}
