using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.UI;

namespace LinkMe.Web.Views.Shared
{
    public class AcceptTerms
        : ViewUserControl<CheckBoxValue>
    {
        protected static string GetPopupHtml()
        {
            return LinkMePage.GetPopupATag(SupportRoutes.Terms.GenerateUrl(), "TermsAndConditions", "terms and conditions", 800, 550);
        }
    }
}
