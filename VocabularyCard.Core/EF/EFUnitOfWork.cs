﻿using System;
using System.Collections;
using System.Data.Entity;
using VocabularyCard.Core.Entities;
using VocabularyCard.Core.Repositories;

namespace VocabularyCard.Core.EF
{
    // todo: 雖然有實作 Dispose，但是好像也沒用到?? 使用上沒有使用 using，也沒有主動呼叫 Dispose
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;
        private Hashtable _repositories;
        private bool _disposed;

        public EFUnitOfWork(IDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            // todo: 這邊如果能改讓 autofac 來處理的話應該比較好
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(EFBaseRepository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));

            return (IRepository<TEntity>)_repositories[type];
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
