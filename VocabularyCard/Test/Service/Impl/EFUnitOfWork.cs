using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Test.Repository;
using VocabularyCard.Test.Repository.EF;

namespace VocabularyCard.Test.Service
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        private bool _disposed;
        private Hashtable _repositories;

        /// <summary>
        /// 設定此Unit of work(UOF)的Context。
        /// </summary>
        /// <param name="context">設定UOF的context</param>
        public EFUnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 儲存所有異動。
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        /// <param name="disposing">是否在清理中？</param>
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

        // 這邊感覺應該要讓 autofac 來注入各個 EFxxxRepository 到 _repositories 才對欸
        // 因為這只是範例嗎?
        // 各 servvice 也要讓 autofac 來幫忙注入要用到的 EFxxxRepository 阿
        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            // 看來是在這邊做 singleton，
            // 假如 Hashtable 裡沒有指定的 repository

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                // 我應該是可以開一個 EFGenericRepository，但不想另外加 CRUD method，
                // 是怕沒有真的太通用的 CRUD method signature，
                // 不過好像也挺多通用的? 新增、讀取、修改 還算通用
                // 除了 刪除(有時不是真的從 DB 刪除，而是在 table 上的欄位將狀態改為 刪除 這樣)
                var repositoryType = typeof(EFBaseRepository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }

    }
}
