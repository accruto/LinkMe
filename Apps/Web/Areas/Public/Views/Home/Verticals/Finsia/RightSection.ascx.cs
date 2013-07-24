using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Finsia
{
    public class RightSection
        : ViewUserControl
    {
        protected static readonly IList<Tuple<string, ReadOnlyUrl>> Groups = new List<Tuple<string, ReadOnlyUrl>>
        {
            new Tuple<string, ReadOnlyUrl>("Finsia - Financial Advising Special Interest Group", new ReadOnlyApplicationUrl("~/groups/finsia-financial-advising-special-interest")),
            new Tuple<string, ReadOnlyUrl>("Finsia - Financial Services Insitute of Australasia", new ReadOnlyApplicationUrl("~/groups/finsia-financial-services-institute-of-australasia")),
            new Tuple<string, ReadOnlyUrl>("Finsia - Young Finance Professionals", new ReadOnlyApplicationUrl("~/groups/finsia-young-finance-professionals")),
        };
    }
}