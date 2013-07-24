using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinkMe.Apps.Agents.Featured.Queries;
using LinkMe.Apps.Asp.Caches;
using LinkMe.Apps.Asp.Content;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Environment;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Areas.Shared.Models;

namespace LinkMe.Web.Areas.Shared
{
    public class HomeRefreshCacheManager
        : IRefreshCacheManager
    {
        private static readonly EventSource EventSource = new EventSource<HomeRefreshCacheManager>();
        private readonly IFeaturedQuery _featuredQuery;

        public HomeRefreshCacheManager(IFeaturedQuery featuredQuery)
        {
            _featuredQuery = featuredQuery;
        }

        object IRefreshCacheManager.GetItem(string key)
        {
            const string method = "GetItem";
            if (EventSource.IsEnabled(Event.Information))
                EventSource.Raise(Event.Information, method, "The '" + key + "' item is being refreshed.");

            try
            {
                switch (key)
                {
                    case CacheKeys.FeaturedStatistics:
                        return _featuredQuery.GetFeaturedStatistics();

                    case CacheKeys.FeaturedJobAds:
                        return GetFeaturedJobAds();

                    case CacheKeys.FeaturedCandidateSearches:
                        return GetFeaturedCandidateSearches();

                    case CacheKeys.FeaturedEmployers:
                        return GetFeaturedEmployers();

                    default:
                        return 0;
                }
            }
            finally
            {
                if (EventSource.IsEnabled(Event.Information))
                    EventSource.Raise(Event.Information, method, "The '" + key + "' item has been refreshed.");
            }
        }

        private IList<FeaturedEmployerModel> GetFeaturedEmployers()
        {
            return (from e in _featuredQuery.GetFeaturedEmployers()
                    orderby e.LogoOrder
                    select new FeaturedEmployerModel
                    {
                        Name = e.Name,
                        SearchUrl = SearchRoutes.Search.GenerateUrl(new {advertiser = e.Name, sortOrder = 2}),
                        LogoUrl = new ContentUrl(StaticEnvironment.GetFileVersion(Assembly.GetExecutingAssembly()), e.LogoUrl)
                    }).ToList();
        }

        private IList<FeaturedLinkModel> GetFeaturedJobAds()
        {
            return (from j in _featuredQuery.GetFeaturedJobAds()
                    select new FeaturedLinkModel
                    {
                        Url = new ReadOnlyApplicationUrl(true, j.Url),
                        Title = j.Title,
                    }).ToList();
        }

        private IList<FeaturedLinkModel> GetFeaturedCandidateSearches()
        {
            return (from j in _featuredQuery.GetFeaturedCandidateSearches()
                    select new FeaturedLinkModel
                    {
                        Url = new ReadOnlyApplicationUrl(true, j.Url),
                        Title = j.Title,
                    }).ToList();
        }
    }
}