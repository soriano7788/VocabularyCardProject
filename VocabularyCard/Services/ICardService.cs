﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.DTO;
using VocabularyCard.Core.Services;
using VocabularyCard.Dtos;

namespace VocabularyCard.Services
{
    public interface ICardService : IService
    {
        CardDto GetById(int cardId);
        CardDto[] GetCardsByCardSetId(UserInfo userInfo, int cardSetId);
    }
}
