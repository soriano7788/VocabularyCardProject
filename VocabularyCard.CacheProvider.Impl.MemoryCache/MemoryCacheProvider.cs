using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace VocabularyCard.CacheProvider.Impl.InMemoryCache
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private static readonly MemoryCache _cache = MemoryCache.Default;
        public T Get<T>(string key) where T : class
        {
            return _cache.Get(key) as T;
        }

        public void Set(string key, object value)
        {
            Set(key, value, 0);
        }

        public void Set(string key, object value, int expirationMinutes)
        {
            var policy = new CacheItemPolicy();
            if(expirationMinutes > 0)
            {
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expirationMinutes);
            }

            _cache.Set(key, value, policy);
        }
    }
}
