using System;
using System.Web.Caching;

namespace LinkMe.Apps.Asp.Caches
{
    public class CacheManager
        : ICacheManager
    {
        private readonly IRefreshCacheManager _refreshCacheManager;
        private readonly TimeSpan _timeOfDay;

        public CacheManager(IRefreshCacheManager refreshCacheManager, TimeSpan timeOfDay)
        {
            _refreshCacheManager = refreshCacheManager;
            _timeOfDay = timeOfDay;
        }

        T ICacheManager.GetCachedItem<T>(Cache cache, string key)
        {
            var item = cache.Get(key);
            if (item != null)
                return (T) item;

            item = _refreshCacheManager.GetItem(key);

            var nextExpirationTime = DateTime.Today.Add(_timeOfDay);
            if (nextExpirationTime <= DateTime.Now)
                nextExpirationTime = nextExpirationTime.AddDays(1);

            cache.Insert(key, item, null, nextExpirationTime, Cache.NoSlidingExpiration);
            return (T)item;
        }
    }
}
