using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Domain.Users.Members.Search;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Content;
using LinkMe.Web.Helper;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Web.UI.Controls.Employers;

namespace LinkMe.Web.UI.Registered.Employers
{
	public partial class SavedResumeSearchesControl
        : LinkMeUserControl
	{
	    private static readonly IMemberSearchesCommand _memberSearchesCommand = Container.Current.Resolve<IMemberSearchesCommand>();
        private static readonly IMemberSearchesQuery _memberSearchesQuery = Container.Current.Resolve<IMemberSearchesQuery>();
        private static readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand = Container.Current.Resolve<IMemberSearchAlertsCommand>();
        private static readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery = Container.Current.Resolve<IMemberSearchAlertsQuery>();

        public event SavedResumeSearchEventHandler SavedResumeSearchAction;
	    public bool ColumnarLayout { get; set; }
        private Repeater _activeRepeater;

        private IList<MemberSearch> _items;
        private bool _showEmptyListMessage = true;

	    protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
 
            AddJavaScriptReference(JavaScripts.Scriptaculous);
            AddJavaScriptReference(JavaScripts.ScrollTracker);
            AddJavaScriptReference(JavaScripts.StringUtils);
            AddJavaScriptReference(JavaScripts.PrototypeValidation);
            AddJavaScriptReference(JavaScripts.OverlayPopup);
            AddJavaScriptReference(JavaScripts.LocateHelper);
            AddJavaScriptReference(JavaScripts.SectionsEditor);
            AddJavaScriptReference(JavaScripts.AjaxHelper);

            AddStyleSheetReference(StyleSheets.SavedResumeSearches);

            _activeRepeater = ColumnarLayout ? rptSavedResumeSearchesColumnar : rptSavedResumeSearches;

            _activeRepeater.ItemDataBound += SavedCandidateSearchesRepeater_ItemDataBound;
			BindSavedResumeSearchesRepeater();

            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxMemberSearchEditor));
		}

        protected int GetColumnIndex(int itemIndex, int columnCount)
        {
            return (int)Math.Floor(((float)itemIndex / _items.Count * columnCount));
        }

        protected bool IsNewColumn(int itemIndex, int columnCount)
        {
            return GetColumnIndex(itemIndex, columnCount) != GetColumnIndex(itemIndex - 1, columnCount);
        }

        protected bool IsNamed(object r)
        {
            return !((MemberSearch)r).Name.IsNullOrEmpty();
        }

        public int MemberSearchCount()
        {
            if (LoggedInEmployer == null)
                return 0;
            return _memberSearchesQuery.GetMemberSearches(LoggedInUserId.Value).Count;
        }

	    public bool ShowEmptyListMessage
	    {
	        get { return _showEmptyListMessage; }
	        set { _showEmptyListMessage = value; }
	    }

	    private void BindSavedResumeSearchesRepeater()
		{
            // This control might on a page where nobody is logged in yet.
            // In that case, don't databind anything.

            if (LoggedInEmployer == null)
            {
                phNotLoggedIn.Visible = true;
                return;
            }

            _items = _memberSearchesQuery.GetMemberSearches(LoggedInUserId.Value);

            if (_items.IsNullOrEmpty())
            {
                _activeRepeater.Visible = false;
                phNoItems.Visible = ShowEmptyListMessage;
                return;
            }

			_activeRepeater.DataSource = _items;
			_activeRepeater.DataBind();
            _activeRepeater.Visible = true;
		}

        protected virtual void OnSavedResumeSearchAction(SavedResumeSearchEventArgs e)
        {
            if (SavedResumeSearchAction != null)
            {
                SavedResumeSearchAction(this, e);
            }
        }

	    protected void rptSavedResumeSearches_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			var memberSearchId = new Guid(e.CommandArgument.ToString());
            var memberSearch = _memberSearchesQuery.GetMemberSearch(memberSearchId);

			if (memberSearch == null)
			{
				switch(e.CommandName.ToUpper())
				{
					case "EXECUTE":
						LinkMePage.AddError("The search you wanted to run no longer exists.");
						break;

					case "CREATEEMAILALERT":
						LinkMePage.AddError("The search for which you tried to create an Email Alert no longer exists. " +
											"Your Alert was not created.");
						break;

					default:
						break;
				}

				BindSavedResumeSearchesRepeater();
				return;
			}

            try
            {
                SearchHelper.EnsureEmployerIsJobPoster(LoggedInUserId.Value, memberSearch);
            }
            catch (UserException ex)
            {
                LinkMePage.AddError(ex.Message);
                return;
            }

			switch(e.CommandName.ToUpper())
			{
				case null:
					break;
					
				case "EXECUTE":
			        var url = SearchRoutes.Saved.GenerateUrl(new {savedSearchId = memberSearch.Id});
                    NavigationManager.Redirect(url);
					break;
					
				case "CREATEEMAILALERT":

                    // Update it.

                    _memberSearchAlertsCommand.UpdateMemberSearch(LoggedInUser, memberSearch, new Tuple<AlertType, bool>(AlertType.Email, true));
					BindSavedResumeSearchesRepeater();
					OnSavedResumeSearchAction(new SavedResumeSearchEventArgs("The search \"" + memberSearch.GetDisplayHtml() + "\" has been made into an email alert."));
					break;

				case "REMOVE":
                    _memberSearchAlertsCommand.DeleteMemberSearch(LoggedInUser, memberSearch);
					BindSavedResumeSearchesRepeater();
					
					if(_items != null && _items.Count > 0)
                        OnSavedResumeSearchAction(new SavedResumeSearchEventArgs("The search \"" + memberSearch.GetDisplayHtml() + "\" has been removed."));
					break;

				default:
					break;
			}
		}

	    private static void SavedCandidateSearchesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				var search = (MemberSearch)e.Item.DataItem;
                var litDescription = (Literal)e.Item.FindControl("litDescription");
                var phCreateAlert = (PlaceHolder)e.Item.FindControl("phCreateAlert");
                var lblAlertExists = (Label)e.Item.FindControl("lblAlertExists");
				
				if (litDescription != null) litDescription.Text = search.GetDisplayHtml();

			    var alert = _memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.Email);
				
				if (alert == null)
				{
                    if (phCreateAlert != null) phCreateAlert.Visible = true;
                    if (lblAlertExists != null) lblAlertExists.Visible = false;
				}
				else
				{
                    if (phCreateAlert != null) phCreateAlert.Visible = false;
                    if (lblAlertExists != null) lblAlertExists.Visible = true;					
				}
				
			}
		}		
		
		#region Events

		public delegate void SavedResumeSearchEventHandler(object sender, SavedResumeSearchEventArgs e);

		public class SavedResumeSearchEventArgs
            : EventArgs
		{
			private readonly string _message;

			public SavedResumeSearchEventArgs(string message)
			{
				_message = message;
			}
			
			public string Message
			{
				get { return _message; }
			}
		}
		
		#endregion 
	}
}
