namespace LinkMe.Apps.Asp.Caches
{
    public interface IRefreshCacheManager
    {
        object GetItem(string key);
    }
}
