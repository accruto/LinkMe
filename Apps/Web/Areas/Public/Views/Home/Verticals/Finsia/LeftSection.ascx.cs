using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Finsia
{
    public class LeftSection
        : ViewUserControl
    {
        protected static readonly IList<Tuple<string, ReadOnlyUrl>> Searches = new List<Tuple<string, ReadOnlyUrl>>
        {
            new Tuple<string, ReadOnlyUrl>("Accounting &amp; Finance Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AnyKeywords=Accounting%2c+Finance&CountryId=1&IndustryIds=fa9b69c7-4a3f-498c-a2c4-42addfb08f7d%2ce7b9bb14-b0e3-4fa1-b055-22751c3f7de2&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Asset Management Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Asset+Management&CountryId=1&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Corporate Banking Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Corporate+Banking&CountryId=1&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Graduate Jobs &amp; Internships", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AnyKeywords=Graduate%2c+Intern&CountryId=1&IndustryIds=fa9b69c7-4a3f-498c-a2c4-42addfb08f7d%2ce7b9bb14-b0e3-4fa1-b055-22751c3f7de2%2c830f50b4-6f2a-436d-b5d4-9c29fd748d62&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Insurance Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&AllKeywords=insurance&CountryId=1&IndustryIds=830f50b4-6f2a-436d-b5d4-9c29fd748d62&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Investment Banking", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Investment+banking&CountryId=1&SortOrder=1&JobTypes=31")),
            new Tuple<string, ReadOnlyUrl>("Risk Management", new ReadOnlyApplicationUrl("~/search/jobs/advanced?performSearch=True&ExactPhrase=Risk+Management&CountryId=1&SortOrder=1&JobTypes=31")),
        };
    }
}