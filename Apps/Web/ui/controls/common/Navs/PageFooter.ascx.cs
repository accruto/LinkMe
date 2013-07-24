using System;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web.UI.Controls.Common.Navs
{
	public partial class PageFooter
        : NavUserControl
	{
        protected UserType ActiveUserType { get; private set; }

        protected static ReadOnlyUrl AboutUrl { get { return SupportRoutes.AboutUs.GenerateUrl(); } }
        protected static ReadOnlyUrl TermsUrl { get { return SupportRoutes.Terms.GenerateUrl(); } }
        protected static ReadOnlyUrl PrivacyUrl { get { return SupportRoutes.Privacy.GenerateUrl(); } }
        protected static ReadOnlyUrl EmployerHomeUrl { get { return Areas.Employers.Routes.HomeRoutes.Home.GenerateUrl(); } }
        protected static ReadOnlyUrl MemberHomeUrl { get { return HomeRoutes.Home.GenerateUrl(); } }
        protected static ReadOnlyUrl EmployerSiteHomeUrl { get { return Areas.Employers.Routes.HomeRoutes.Home.GenerateUrl(new { ignorePreferred = true }); } }
        protected static ReadOnlyUrl MemberSiteHomeUrl { get { return HomeRoutes.Home.GenerateUrl(new { ignorePreferred = true }); } }
        protected static ReadOnlyUrl HomeUrl { get { return HomeRoutes.Home.GenerateUrl(); } }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ActiveUserType = LinkMePage.ActiveUserType;
        }
	}
}
