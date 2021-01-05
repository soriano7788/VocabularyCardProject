using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Entities;

namespace VocabularyCard.Entities
{
    public class CardSet : BaseEntity
    {
        public int CardSetId { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public string Owner { get; set; }
        public CardSetState State { get; set; }
        /// <summary>
        /// Navigation properties are typically defined as virtual 
        /// so that they can take advantage of certain Entity Framework functionality 
        /// such as lazy loading. (Lazy loading will be explained later, 
        /// in the Reading Related Data tutorial later in this series.)
        /// </summary>
        public virtual ICollection<Card> Cards { get; set; }
    }
}
