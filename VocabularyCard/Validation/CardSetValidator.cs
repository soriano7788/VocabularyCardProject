﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Dtos;

namespace VocabularyCard.Validation
{
    public class CardSetValidator
    {
        public static ValidationResult IsValid(CardSetDto cardSetDto, ValidationContext validationContext)
        {
            return ValidationResult.Success;
            return new ValidationResult("資料無效");
        }


    }
}
