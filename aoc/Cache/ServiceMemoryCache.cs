using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace aoc.Cache
{
    public class ServiceMemoryCache : IMemoryCache
    {
        private readonly IMemoryCache _cache;
        public ServiceMemoryCache() => _cache = new MemoryCache(new MemoryCacheOptions());

        public bool TryGetValue(object key, out object value)
        {
            return _cache.TryGetValue(key, out value);
        }

        public ICacheEntry CreateEntry(object key) => _cache.CreateEntry(key);
        public void Remove(object key) => _cache.Remove(key);
        public void Dispose() => _cache.Dispose();
    }

}
