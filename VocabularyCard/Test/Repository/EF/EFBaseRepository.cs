using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Test.Repository.EF
{
    /// <summary>
    /// 實作Entity Framework Generic Repository 的 Class。
    /// </summary>
    /// <typeparam name="TEntity">EF Model 裡面的Type</typeparam>

    public class EFBaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private IDbSet<TEntity> _dbEntitySet;

        /// <summary>
        /// 建構EF一個Entity的Repository，需傳入此Entity的Context。  // 就是一個 繼承 DbContext 的 class 產生的 instance
        /// </summary>
        /// <param name="context">Entity所在的Context</param>

        public EFBaseRepository(DbContext context)
        {
            // context 實際上是 EFCardSetDao 之類的 instance
            _context = context;
            _dbEntitySet = _context.Set<TEntity>();
        }

        /// <summary>
        /// 新增一筆資料到資料庫。
        /// </summary>
        /// <param name="entity">要新增到資料的庫的Entity</param>
        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
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

        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <returns>Entity全部筆數的IQueryable。</returns>
        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// 更新一筆Entity內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        public void Update(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 更新一筆Entity的內容。只更新有指定的Property。
        /// </summary>
        /// <param name="entity">要更新的內容。</param>
        /// <param name="updateProperties">需要更新的欄位。</param>
        public void Update(TEntity entity, Expression<Func<TEntity, object>>[] updateProperties)
        {
            _context.Configuration.ValidateOnSaveEnabled = false;

            _context.Entry<TEntity>(entity).State = EntityState.Unchanged;

            if (updateProperties != null)
            {
                foreach (var property in updateProperties)
                {
                    _context.Entry<TEntity>(entity).Property(property).IsModified = true;
                }
            }
        }


        // 刪除先不寫，因為刪除實際處理的方式可能有不同做法
        // 1. 真的從 DB 刪除
        // 2. 例如這筆資料有一個 status 欄位，把他的 status 值改成 remove



        /// <summary>
        /// 儲存異動。
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();

            // 因為Update 單一model需要先關掉validation，因此重新打開
            if (_context.Configuration.ValidateOnSaveEnabled == false)
            {
                _context.Configuration.ValidateOnSaveEnabled = true;
            }
        }
    }
}
