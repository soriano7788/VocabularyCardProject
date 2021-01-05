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
    public interface ICardSetService : IService
    {
        CardSetDto GeyById(int id);
        CardSetDto[] GetAll();
        CardSetDto[] GetByOwner(UserInfo owner);
        CardDto[] GetCardsByCardSetId(UserInfo userInfo, int cardSetId);

        CardSetDto Create(CardSetDto cardSetInfo);
    }
}