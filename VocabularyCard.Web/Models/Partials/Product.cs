using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VocabularyCard.Web.Models.Partials
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        public class ProductMetaData
        {
            [Required]
            public string Name;
        }
    }
}