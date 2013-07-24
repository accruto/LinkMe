using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Scouts
{
    public class LeftSection
        : ViewUserControl
    {
        private static readonly string[] States = new[] {"VIC", "NSW", "QLD", "NT", "WA", "SA", "TAS"};

        protected static readonly IList<Tuple<string, ReadOnlyUrl>> Searches =
            (from s in States
            select new Tuple<string, ReadOnlyUrl>(
                "Search jobs in " + s,
                new ReadOnlyApplicationUrl("~/search/jobs/advanced", new ReadOnlyQueryString("performSearch", "True", "CountryId", "1", "Location", s, "SortOrder", "1", "JobTypes", "31")))).ToList();
    }
}
