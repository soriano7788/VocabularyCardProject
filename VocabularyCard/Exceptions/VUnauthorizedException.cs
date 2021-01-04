using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Exceptions
{
    public class VUnauthorizedException : Exception
    {
        public VUnauthorizedException() { }
        public VUnauthorizedException(string message) : base(message) { }
        public VUnauthorizedException(string message, Exception inner) : base(message, inner) { }
    }
}
