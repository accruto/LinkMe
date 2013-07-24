using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Administrators.Models.Organisations;
using LinkMe.Web.Areas.Administrators.Routes;

namespace LinkMe.Web.Areas.Administrators.Views.OrganisationReports
{
    public class Report
        : Apps.Asp.Mvc.Views.ViewPage<ReportModel>
    {
        protected static ReadOnlyUrl CalendarButtonUrl = new ReadOnlyApplicationUrl("~/ui/images/buttons/calendar.gif");

        protected MvcHtmlString GetAccountManagerHtml()
        {
            return Wrap(Html.EmailLink(Model.AccountManager.FullName, Model.AccountManager.EmailAddress.Address));
        }

        protected MvcHtmlString GetClientHtml()
        {
            return string.IsNullOrEmpty(Model.ContactDetails.EmailAddress)
                ? Wrap(Html.RouteRefLink("(email not set)", OrganisationsRoutes.Edit, new {id = Model.Organisation.Id}))
                : Wrap(Html.EmailLink(Model.ContactDetails.FullName ?? Model.ContactDetails.EmailAddress, Model.ContactDetails.EmailAddress));
        }

        private static MvcHtmlString Wrap(MvcHtmlString link)
        {
            return MvcHtmlString.Create("<label>" + link + "</label>");
        }
    }
}