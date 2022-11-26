using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.Configurations;
using System;
using System.Collections.Generic;

namespace PaparaThirdWeek.Services.Concretes
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheConfiguration _cacheConfig;
        private MemoryCacheEntryOptions _cacheEntryOptions; //cachin ne kadar ayakta kalacağını söyleyen class

        public CacheService(IMemoryCache memoryCache, IOptions<CacheConfiguration> cacheConfig)
        {
            _memoryCache = memoryCache;
            _cacheConfig = cacheConfig.Value;
            if (_cacheConfig != null)
            {
                _cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(_cacheConfig.AbsoluteExpirationInHours),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(_cacheConfig.SlidingExpritionInMinutes)
                };
            }
        }

        public bool TryGet<T>(string cachekey, out List<T> value)
        {
            _memoryCache.TryGetValue(cachekey, out value);
            if (value == null) return false;
            else return true;
        }

        public void Remove(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

        public T Set<T>(string cacheKey, T value)
        {
            return _memoryCache.Set(cacheKey, value, _cacheEntryOptions);
        }

        public bool TryGet<T>(string cacheKey, out T value)
        {
            _memoryCache.TryGetValue(cacheKey, out value);
            if (value == null) return false;
            else return true;
        }
    }
}
