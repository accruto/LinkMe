using System.Web.Caching;

namespace LinkMe.Apps.Asp.Caches
{
    public interface ICacheManager
    {
        T GetCachedItem<T>(Cache cache, string key);
    }
}