using System;
using System.Web.Caching;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Management.Areas.Communications.Models;

namespace LinkMe.Apps.Management.Areas.Communications.Controllers
{
    public abstract class CommunicationsController
        : ViewController
    {
        private const int ExpirationDurationHours = 12;

        protected T GetCachedItem<T>(string key)
            where T : class
        {
            return HttpContext.Cache.Get(key) as T;
        }

        protected void InsertCachedItem<T>(string key, T item)
            where T : class
        {
            HttpContext.Cache.Insert(key, item, null, DateTime.Now.AddHours(ExpirationDurationHours), Cache.NoSlidingExpiration);
        }

        protected T GetCachedItem<T>(string key, Func<T> getItem)
            where T : class
        {
            var t = GetCachedItem<T>(key);
            if (t != null)
                return t;

            // Not found in cache, need to insert.

            t = getItem();
            InsertCachedItem(key, t);
            return t;
        }

        protected TModel CreateModel<TModel>(CommunicationsContext context)
            where TModel : CommunicationsModel, new()
        {
            return new TModel
            {
                Definition = context.Definition,
                Category = context.Category,
                ContextId = context.ContextId,
                UserId = context.UserId,
                IsPreview = context.IsPreview,
            };
        }
    }
}
