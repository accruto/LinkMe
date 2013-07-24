using System;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Registered.Employers
{
    public partial class EmployerJobAdsSuggestions : LinkMePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UseStandardStyleSheetReferences = false;

            AddStyleSheetReference(StyleSheets.UniversalLayout);
            AddStyleSheetReference(StyleSheets.Fonts);
            AddStyleSheetReference(StyleSheets.TextLinksHeadings);
            AddStyleSheetReference(StyleSheets.WidgetsAndLists);
            AddStyleSheetReference(StyleSheets.Sidebar);
            AddStyleSheetReference(StyleSheets.Forms);
            AddStyleSheetReference(StyleSheets.Forms2);
            AddStyleSheetReference(StyleSheets.Messaging);

            AddStyleSheetReference(StyleSheets.SidebarOnLeft);
            AddStyleSheetReference(StyleSheets.HeaderAndNav);
            AddStyleSheetReference(StyleSheets.Employer);
            AddStyleSheetReference(StyleSheets.Search);

            employerJobAdsControl.ViewOnly = true;
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return new[] { UserType.Employer }; }
        }

        protected override bool GetRequiresActivation()
        {
            return true;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Employer;
        }
    }
}
