using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Content;
using LinkMe.Web.UI;
using LinkMe.Apps.Asp.Navigation;

namespace LinkMe.Web.Landing
{
    public partial class SampleJoin : LinkMePage
    {
        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }

        protected override bool GetRequiresActivation()
        {
            return false;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            UseStandardStyleSheetReferences = false;
            AddStyleSheetReference(StyleSheets.LandingSample);
        }

        protected static string ActionUrl
        {
            get { return NavigationManager.GetUrlForPage<Join>().AbsoluteUri; }
        }

        protected static string TermsAndConditionsPopup
        {
            get { return GetPopupATag(SupportRoutes.Terms.GenerateUrl(), "Terms and conditions", "terms and conditions"); }
        }
    }
}
