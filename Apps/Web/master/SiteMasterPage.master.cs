using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Apps.Asp.UI;
using LinkMe.Web.Html;
using LinkMe.Web.UI.Controls.Common;
using LinkMe.WebControls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Master
{
    public partial class SiteMasterPage
        : MasterPage, ILinkMeMasterPage
    {
        private static readonly HtmlUtilsHelper HtmlHelper = new HtmlUtilsHelper();
        private static readonly ReadOnlyUrl JavascriptUrl = new ReadOnlyApplicationUrl("~/js/Javascript.aspx");
        private ReadOnlyUrl _clientUrl;

        protected static HtmlUtilsHelper Html
        {
            get { return HtmlHelper; }
        }

        protected static ReadOnlyUrl GetJavascriptUrl()
        {
            return JavascriptUrl;
        }

        protected string PageIdentifier
        {
            get
            {
                // The Page type is an ASP.NET generated type which derives from the type defined in the code.

                return Page.GetType().BaseType.FullName;
            }
        }

        protected override ReadOnlyUrl GetClientUrl()
        {
            if (_clientUrl == null)
                _clientUrl = base.GetClientUrl();
            return _clientUrl;
        }

        protected ReadOnlyUrl GetCanonicalUrl()
        {
            return GetClientUrl().GetCanonicalUrl();
        }

        LinkMeValidationSummary ILinkMeMasterPage.ValidationSummary
        {
            get { return null; }
        }

        SideBarContainer ILinkMeMasterPage.SideBarContainer
        {
            get { return null; }
        }

        HtmlForm ILinkMeMasterPage.MainForm
        {
            get { return null; }
        }

        PlaceHolder ILinkMeMasterPage.NonFormPlaceHolder
        {
            get { return null; }
        }

        ExplicitClientIdHtmlControl ILinkMeMasterPage.FormContainer
        {
            get { return null; }
        }
    }
}
