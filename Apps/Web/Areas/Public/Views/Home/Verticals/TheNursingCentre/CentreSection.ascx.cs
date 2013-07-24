using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.TheNursingCentre
{
    public class CentreSection
        : ViewUserControl
    {
        protected static readonly IList<Tuple<string, ReadOnlyUrl>> Searches = new List<Tuple<string, ReadOnlyUrl>>
        {
            new Tuple<string, ReadOnlyUrl>("All Nursing jobs", new ReadOnlyApplicationUrl("~/search/jobs?performSearch=True&Keywords=nurse")),
            new Tuple<string, ReadOnlyUrl>("All Midwifery jobs", new ReadOnlyApplicationUrl("~/search/jobs?performSearch=True&Keywords=midwife")),
            new Tuple<string, ReadOnlyUrl>("Aged Care Nursing", new ReadOnlyApplicationUrl("~/search/jobs?performSearch=True&Keywords=aged+care+nurse")),
            new Tuple<string, ReadOnlyUrl>("Mental Health Nursing", new ReadOnlyApplicationUrl("~/search/jobs?performSearch=True&Keywords=mental+health+nurse")),
            new Tuple<string, ReadOnlyUrl>("Emergency Nursing", new ReadOnlyApplicationUrl("~/search/jobs?performSearch=True&Keywords=emergency+nurse")),
            new Tuple<string, ReadOnlyUrl>("Community Nursing", new ReadOnlyApplicationUrl("~/search/jobs?performSearch=True&Keywords=community+nurse")),
        };
    }
}
