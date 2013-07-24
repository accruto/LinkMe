using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Ahri
{
    public class LeftSection
        : ViewUserControl
    {
        protected static readonly IList<Tuple<string, ReadOnlyUrl>> Searches =
            new List<Tuple<string, ReadOnlyUrl>>
                {
                    new Tuple<string, ReadOnlyUrl>("General HR Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced", new ReadOnlyQueryString("performSearch", "True", "AllKeywords", "HR", "CountryId", "1", "IndustryIds", "995542b4-11f8-401e-b288-300fc9f6e376", "SortOrder", "1", "JobTypes", "31"))),
                    new Tuple<string, ReadOnlyUrl>("Graduate HR &amp; Recruitment Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced", new ReadOnlyQueryString("performSearch", "True", "AllKeywords", "HR+graduate", "CountryId", "1", "IndustryIds", "995542b4-11f8-401e-b288-300fc9f6e376", "SortOrder", "1", "JobTypes", "31"))),
                    new Tuple<string, ReadOnlyUrl>("Industrial Relations Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced", new ReadOnlyQueryString("performSearch", "True", "ExactPhrase", "Industrial+Relations", "CountryId", "1", "IndustryIds", "995542b4-11f8-401e-b288-300fc9f6e376", "SortOrder", "1", "JobTypes", "31"))),
                    new Tuple<string, ReadOnlyUrl>("Learning &amp; Development Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced", new ReadOnlyQueryString("performSearch", "True", "ExactPhrase", "Learning+and+development", "CountryId", "1", "IndustryIds", "995542b4-11f8-401e-b288-300fc9f6e376", "SortOrder", "1", "JobTypes", "31"))),
                    new Tuple<string, ReadOnlyUrl>("OH&amp;S Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced", new ReadOnlyQueryString("performSearch", "True", "ExactPhrase", "OH%26S", "CountryId", "1", "IndustryIds", "995542b4-11f8-401e-b288-300fc9f6e376", "SortOrder", "1", "JobTypes", "31"))),
                    new Tuple<string, ReadOnlyUrl>("Recruitment Consultant Jobs", new ReadOnlyApplicationUrl("~/search/jobs/advanced", new ReadOnlyQueryString("performSearch", "True", "AdTitle", "Recruitment+Consultant", "CountryId", "1", "SortOrder", "1", "JobTypes", "31"))),
                };
    }
}
