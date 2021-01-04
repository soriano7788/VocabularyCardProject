using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VocabularyCard.Web.Filters
{
    public class IgnoreFilterAttribute : Attribute
    {
        public Type FilterType { get; }

        public IgnoreFilterAttribute(Type filterType)
        {
            this.FilterType = filterType;
        }
    }
}
