using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Ping
{
    public class LeftSection
        : ViewUserControl
    {
        protected static readonly IList<Tuple<string, ReadOnlyUrl>> Searches = new List<Tuple<string, ReadOnlyUrl>>
        {
           new Tuple<string, ReadOnlyUrl>("Browse full time jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&CountryId=1&SortOrder=1&JobTypes=1")),
           new Tuple<string, ReadOnlyUrl>("Browse part time jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&CountryId=1&SortOrder=1&JobTypes=2")),
           new Tuple<string, ReadOnlyUrl>("Browse contract jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&CountryId=1&SortOrder=1&JobTypes=4")),
           new Tuple<string, ReadOnlyUrl>("Browse temp jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&CountryId=1&SortOrder=1&JobTypes=8")),
           new Tuple<string, ReadOnlyUrl>("Browse job share jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&CountryId=1&SortOrder=1&JobTypes=16")),
        };
    }
}