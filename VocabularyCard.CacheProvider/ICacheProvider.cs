using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.CacheProvider
{
    public interface ICacheProvider
    {
        T Get<T>(string key) where T : class;
        void Set(string key, object value);

        // todo: cache 的回收還有其他機制，目前只設定存活時間
        void Set(string key, object value, int expirationMinutes);
    }
}
