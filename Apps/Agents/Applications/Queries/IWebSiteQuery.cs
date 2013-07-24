using System;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Agents.Applications.Queries
{
    public interface IWebSiteQuery
    {
        ReadOnlyUrl GetUrl(WebSite webSite, Guid? verticalId, bool secure, string applicationPath);
        ReadOnlyUrl GetUrl(WebSite webSite, Guid? verticalId, bool secure, string applicationPath, ReadOnlyQueryString queryString);
        ReadOnlyUrl GetLoginUrl(WebSite webSite, Guid? verticalId, string loginApplicationPath, string applicationPath);
        ReadOnlyUrl GetLoginUrl(WebSite webSite, Guid? verticalId, string loginApplicationPath, string applicationPath, ReadOnlyQueryString queryString);
    }
}