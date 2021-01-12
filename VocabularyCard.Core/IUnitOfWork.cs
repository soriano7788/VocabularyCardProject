using System;
using VocabularyCard.Core.Entities;
using VocabularyCard.Core.Repositories;

namespace VocabularyCard.Core
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 儲存所有異動。
        /// </summary>
        void Save();
        /// <summary>
        /// 取得某一個 Entity 的 Repository。
        /// 如果此 Entity 的 Repository 不存在，就 initialize 一個，並存下來
        /// 如果已存在，就拿出來 return。
        /// </summary>
        /// <typeparam name="TEntity">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    }
}
