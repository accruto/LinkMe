using LinkMe.Web.Content;
using LinkMe.Web.Manager.Navigation;
using LinkMe.Web.UI;
using LinkMe.Apps.Asp.UI;
using LinkMe.Apps.Asp.Urls;

namespace LinkMe.Web.Master
{
    public partial class HomeMasterPage : LinkMeNestedMasterPage
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            ((IMasterPage)this).AddStyleSheetReference(StyleSheets.HomePages);
        }
    }
}
