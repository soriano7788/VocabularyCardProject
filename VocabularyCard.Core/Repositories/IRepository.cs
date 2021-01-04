using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Entities;

namespace VocabularyCard.Core.Repositories
{
    public interface IRepository<T> where T : IEntity
    {

        T Create(T entity);
        T Update(T entity);
        IList<T> GetAll();
        T Read(Expression<Func<T, bool>> predicate);
    }
}
