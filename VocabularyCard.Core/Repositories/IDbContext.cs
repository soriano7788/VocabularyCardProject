using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Core.Entities;

namespace VocabularyCard.Core.Repositories
{
    public interface IDbContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : BaseEntity;

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
