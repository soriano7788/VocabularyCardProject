using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Entities;
using System.Data.Entity;
using System.Linq.Expressions;
using VocabularyCard.Core.Repositories;

namespace VocabularyCard.Core.EF
{
    public class EFBaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private IDbContext _context;
        private IDbSet<TEntity> _entitySet;

        public EFBaseRepository(IDbContext context)
        {
            _context = context;
            _entitySet = _context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            return _entitySet.Add(entity);
        }

        public TEntity[] CreateMany(TEntity[] entities)
        {
            return (_entitySet as DbSet<TEntity>).AddRange(entities).ToArray();
        }

        public IList<TEntity> GetAll()
        {
            return _entitySet.ToList();
        }

        /// <summary>
        /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得第一筆符合條件的內容。</returns>
        public TEntity Read(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public virtual TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
