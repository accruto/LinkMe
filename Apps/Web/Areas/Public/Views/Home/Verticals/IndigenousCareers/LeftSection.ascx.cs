using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.IndigenousCareers
{
    public class LeftSection
        : ViewUserControl
    {
        protected static readonly IList<Tuple<string, ReadOnlyUrl>> Searches = new List<Tuple<string, ReadOnlyUrl>>
        {
            new Tuple<string, ReadOnlyUrl>("All Indigenous Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=Indigenous&CountryId=1&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Indigenous Health Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=Indigenous&CountryId=1&IndustryIds=180a913d-d05b-49f1-b3c9-45d57368a3ef&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Indigenous Education Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=Indigenous&CountryId=1&IndustryIds=e1425949-7e37-48db-afc3-4411e08466f3&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Indigenous Management Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=Indigenous+management&CountryId=1&SortOrder=1&JobTypes=31")),
        };
    }
}