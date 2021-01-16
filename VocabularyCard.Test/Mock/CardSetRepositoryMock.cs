using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Entities;
using VocabularyCard.Repositories;

namespace VocabularyCard.Test.Mock
{
    public class CardSetRepositoryMock : ICardSetRepository
    {
        public CardSet Create(CardSet entity)
        {
            throw new NotImplementedException();
        }

        public IList<CardSet> GetAll()
        {
            throw new NotImplementedException();
        }

        public CardSet GetByCardSetId(int cardSetId)
        {
            throw new NotImplementedException();
        }

        public IList<CardSet> GetByOwner(string ownerId)
        {
            throw new NotImplementedException();
        }

        public CardSet Read(Expression<Func<CardSet, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CardSet Update(CardSet cardSet)
        {
            throw new NotImplementedException();
        }
    }
}
