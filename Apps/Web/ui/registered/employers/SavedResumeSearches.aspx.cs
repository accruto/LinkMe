using System;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Registered.Employers
{
	public partial class SavedResumeSearches : LinkMePage
	{
        protected static ReadOnlyUrl NewSavedSearchUrl { get { return SearchRoutes.Search.GenerateUrl(); } }
        
        #region controls

		protected SavedResumeSearchesControl savedResumeSearchesControl;

		#endregion

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

            savedResumeSearchesControl.SavedResumeSearchAction += savedResumeSearchesControl_SavedResumeSearchEvent;
		}

        private void savedResumeSearchesControl_SavedResumeSearchEvent(object sender,
            SavedResumeSearchesControl.SavedResumeSearchEventArgs e)
		{
            AddConfirm(e.Message, false);
		}
	}
}
