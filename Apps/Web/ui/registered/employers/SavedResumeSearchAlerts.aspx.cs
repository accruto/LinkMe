using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Domain.Users.Members.Search;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Content;
using LinkMe.Web.Helper;

namespace LinkMe.Web.UI.Registered.Employers
{
	public partial class SavedResumeSearchAlerts : LinkMePage
	{
        private static readonly IMemberSearchesQuery MemberSearchesQuery = Container.Current.Resolve<IMemberSearchesQuery>();
        private static readonly IMemberSearchAlertsCommand MemberSearchAlertsCommand = Container.Current.Resolve<IMemberSearchAlertsCommand>();
        private static readonly IMemberSearchAlertsQuery MemberSearchAlertsQuery = Container.Current.Resolve<IMemberSearchAlertsQuery>();

        private IList<MemberSearch> _items;

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
            AddStyleSheetReference(StyleSheets.Button);

			BindSavedResumeSearchAlertsRepeater();
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

        private void BindSavedResumeSearchAlertsRepeater()
		{
            // Only include searches that have alerts.

            var searches = MemberSearchesQuery.GetMemberSearches(LoggedInUserId.Value);
		    var alertSearches = from a in MemberSearchAlertsQuery.GetMemberSearchAlerts(from s in searches select s.Id, AlertType.Email) select a.MemberSearchId;
		    _items = (from s in searches
                      where alertSearches.Contains(s.Id)
                      select s).ToList();

            if (_items.IsNullOrEmpty())
			{
				EmptyRepeaterMessage.Visible = true;
                SavedResumeSearchAlertsRepeater.Visible = false; // Needed when a search alert is removed.
                return;
			}
			
			SavedResumeSearchAlertsRepeater.DataSource = _items;
			SavedResumeSearchAlertsRepeater.DataBind();
            SavedResumeSearchAlertsRepeater.Visible = true;
            Divider.Visible = true;
		}				

        protected static string GetSearchUrl(object dataItem)
        {
            return SearchRoutes.Saved.GenerateUrl(new { savedSearchId = ((MemberSearch)dataItem).Id }).ToString();
        }

        protected static string GetSearchDisplayHtml(object dataItem)
        {
            return ((MemberSearch)dataItem).GetDisplayHtml();
        }

	    protected void SavedResumeSearchAlertsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			var savedResumeSearchId = new Guid(e.CommandArgument.ToString());
			var savedResumeSearch = MemberSearchesQuery.GetMemberSearch(savedResumeSearchId);

            try
            {
                SearchHelper.EnsureEmployerIsJobPoster(LoggedInUserId.Value, savedResumeSearch);
            }
            catch (UserException ex)
            {
                AddError(ex.Message);
                return;
            }

			switch (e.CommandName)
			{
				case null:
					break;

                case "Remove":
                    MemberSearchAlertsCommand.DeleteMemberSearchAlert(LoggedInUser, savedResumeSearch, AlertType.Email);
					BindSavedResumeSearchAlertsRepeater();
					
					if (_items != null && _items.Count > 0)
					{
						AddConfirm("The search \"" + savedResumeSearch.GetDisplayHtml() + "\" has been removed.", false);
					}
					
					break;

				default:
					break;
			}
		}
	}
}
