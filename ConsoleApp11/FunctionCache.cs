using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    internal class FunctionCache<TKey, TResult>
    {
        private readonly Dictionary<TKey, CacheItem> cache = new Dictionary<TKey, CacheItem>();
        private readonly TimeSpan cacheDuration;

        public delegate TResult Func<TKey, TResult>(TKey key);

        public FunctionCache(TimeSpan cacheDuration)
        {
            this.cacheDuration = cacheDuration;
        }

        public TResult GetOrAdd(TKey key, Func<TKey, TResult> func)
        {
            if (cache.TryGetValue(key, out var cacheItem) && !IsCacheItemExpired(cacheItem))
            {
                return cacheItem.Value;
            }

            TResult result = func(key);
            AddOrUpdateCacheItem(key, result);
            return result;
        }

        private bool IsCacheItemExpired(CacheItem cacheItem)
        {
            return DateTime.Now - cacheItem.Timestamp > cacheDuration;
        }

        private void AddOrUpdateCacheItem(TKey key, TResult result)
        {
            if (cache.ContainsKey(key))
            {
                cache[key] = new CacheItem(result);
            }
            else
            {
                cache.Add(key, new CacheItem(result));
            }
        }

        private class CacheItem
        {
            public CacheItem(TResult value)
            {
                Value = value;
                Timestamp = DateTime.Now;
            }

            public TResult Value { get; }
            public DateTime Timestamp { get; }
        }
    }
}