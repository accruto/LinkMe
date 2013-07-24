using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.GolfJobs
{
    public class CentreSection
        : ViewUserControl
    {
        protected static readonly IList<Tuple<string, ReadOnlyUrl>> Searches = new List<Tuple<string, ReadOnlyUrl>>
        {
            new Tuple<string, ReadOnlyUrl>("All golf jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=golf&CountryId=1&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Greenkeeper jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=greenkeeper&CountryId=1&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Golf hospitality jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=barman&CountryId=1&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Golf professional jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=golf+professional&CountryId=1&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Golf administration jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=golf+administration&CountryId=1&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Golf retail jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=golf+retail&CountryId=1&SortOrder=1&JobTypes=31")),
        };
    }
}