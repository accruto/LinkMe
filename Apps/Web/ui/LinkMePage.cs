using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Asp.Content;
using LinkMe.Apps.Asp.Notifications;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Partners.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Users.Queries;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Accounts.Routes;
using LinkMe.Web.Helper;
using LinkMe.Web.Master;
using LinkMe.Web.UI.Controls.Common;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Security;
using LinkMe.WebControls;

namespace LinkMe.Web.UI
{
    public abstract class LinkMePage
        : Apps.Asp.UI.Page
	{
        private static readonly IUsersQuery _usersQuery = Container.Current.Resolve<IUsersQuery>();
        protected static readonly IAuthenticationManager _authenticationManager = Container.Current.Resolve<IAuthenticationManager>();
        protected static readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();
        protected static readonly IVerticalsQuery _verticalsQuery = Container.Current.Resolve<IVerticalsQuery>();
        protected static readonly ICommunitiesQuery _communitiesQuery = Container.Current.Resolve<ICommunitiesQuery>();
        private static readonly IPartnersQuery _partnersQuery = Container.Current.Resolve<IPartnersQuery>();
        private static readonly IContentEngine ContentEngine = Container.Current.Resolve<IContentEngine>();
        private static readonly IChannelsQuery _channelsQuery = Container.Current.Resolve<IChannelsQuery>();

        #region Data Members

        private readonly LinkMeRequestValidator _requestValidator = new LinkMeRequestValidator();
        private NavigationSiteMapNode _currentSiteMapNode;

        private static readonly ChannelApp _channelApp = _channelsQuery.GetChannelApp(_channelsQuery.GetChannel("Web").Id, "Web");

        public static ChannelApp ChannelApp
        {
            get { return _channelApp; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// True to validates requests for XML, markup and scripts. Return false not to check this.
        /// </summary>
		protected virtual bool DoRequestValidation
		{
			get
			{
				return true; //err on the side of safety. Disable using page overrides
			}
		}

		public string ApplicationPath
		{
			get { return (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath); }
		}

        protected override StateBag ViewState
        {
            get { throw new NotSupportedException("ViewState is disabled."); }
        }

        protected Guid? LoggedInUserId
        {
            get { return Context.User.Id(); }
        }

        protected UserType LoggedInUserType
        {
            get { return Context.User.UserType(); }
        }

        protected bool LoggedInUserCheckStatus
        {
            get { return Context.User.NeedsReset(); }
        }

        private bool LoggedInUserActivated
        {
            get { return Context.User.IsActivated(); }
        }

        protected RegisteredUser LoggedInUser
        {
            get { return _authenticationManager.GetUser(new HttpContextWrapper(Context)); }
        }

        protected Member LoggedInMember
        {
            get { return (LoggedInUser as Member); }
        }

        protected Employer LoggedInEmployer
        {
            get { return (LoggedInUser as Employer); }
        }

        protected Administrator LoggedInAdministrator
        {
            get { return (LoggedInUser as Administrator); }
        }

        protected Custodian LoggedInCustodian
        {
            get { return (LoggedInUser as Custodian); }
        }

        protected virtual bool ShouldDisplayMessage
        {
            get { return !IsPostBack; }
        }

        #endregion

		#region Static methods

        protected static ReadOnlyUrl GetUrlForPage<T>(params string[] paramPairs)
		{
            return NavigationManager.GetUrlForPage<T>(paramPairs);
		}

        protected static ReadOnlyUrl GetUrlForPage<T>(NameValueCollection queryString)
        {
            return NavigationManager.GetUrlForPage<T>(queryString);
        }

        protected static ReadOnlyUrl GetUrlForPage<T>()
        {
            return NavigationManager.GetUrlForPage<T>();
        }

        protected static ReadOnlyUrl GetUrlForPage(string name, params string[] paramPairs)
        {
            return NavigationManager.GetUrlForPage(name, paramPairs);
        }

        public static string GetPopupATag(ReadOnlyUrl targetUrl, string windowName, string text)
        {
            return GetPopupATag(targetUrl, windowName, text, 700, 500);
        }

        public static string GetPopupATag(ReadOnlyUrl targetUrl, string windowName, string text, int widthPx, int heightPx)
        {
            if (targetUrl == null)
                throw new ArgumentException("The target URL must be specified.");

            var url = targetUrl.ToString();

            var sb = new StringBuilder();
            sb.Append("<a href=\"")
                .Append(url)
                .Append("\" target=\"_blank\" onclick=\"var varWindow = window.open('")
                .Append(url)
                .Append("', '")
                .Append(windowName)
                .Append("', 'width=")
                .Append(widthPx)
                .Append(", height=")
                .Append(heightPx)
                .Append(", resizable=yes, scrollbars=yes, toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, copyhistory=no'); varWindow.focus(); return false;\">")
                .Append(text)
                .Append("</a>");
            return sb.ToString();
        }

		#endregion

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            // Checks.

            CheckAuthorization();
            CheckActivation();
        }

        protected override void OnInit(EventArgs e)
		{
			if (DoRequestValidation)
			{
				Validators.Add(_requestValidator);
			}
            
            base.OnInit(e);
		}

        protected override void OnLoad(EventArgs e)
        {
            // Add a P3P header, which we need for being encapsulated into an iframe

            /*
             * These policies apply to our cookies, and enumerate out to:
             * 
             * IDC - Identifiable content (i.e. username)
             * CUR - Current purpose (logging in)
             * OUR - Our agents (will be the recipients)
             * STP - Stated purpose (loggin in)
             * ONL - Online information (user name is email address)
             * STA - State (mainting state, that is login)
             * 
            */

            // See http://www.w3.org/TR/P3P, with http://www.w3.org/TR/P3P/#compact_policies for primary
            // reference, and http://www.w3.org/TR/P3P/#POLICIES for enumeration - KT, 11/03/08

            Response.AddHeader("P3P", "CP=\"IDC CUR OUR STP ONL STA\"");

            base.OnLoad(e);

            if (ShouldDisplayMessage)
            {
                DisplayMessage();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Look for a content.

            var verticalId = ActivityContext.Current.Vertical.Id;

            // Set the favicon.

            var faviconItem = ContentEngine.GetContentItem<ImageContentItem>("Favicon", verticalId);
            if (faviconItem != null)
            {
                var rootFolder = faviconItem.RootFolder;
                if (!rootFolder.EndsWith("/"))
                    rootFolder += "/";
                SetFaviconReference(new FaviconReference(rootFolder + faviconItem.RelativePath));
            }

            UpdatePageHeader();

            base.OnPreRender(e);
        }

		protected void JumpToSection(string section)
		{
			var script = @"<script language='javascript'>location.href='#" + section + "'</script>";
            ClientScript.RegisterStartupScript(typeof(LinkMePage), "JumpToSection", script);
		}

        /// <summary>
        /// Sets the default button for the entire page.
        /// </summary>
        protected void SetDefaultButton(IButtonControl button)
        {
            if (button == null)
                throw new ArgumentNullException("button");
            if (!(button is Control))
                throw new ArgumentException("The button is not a Control.", "button");

            var form = MainForm;
            form.DefaultButton = FieldInputHelper.GetRelativeId(((Control)button).UniqueID, form, IdSeparator);
        }

        /// <summary>
        /// Sets the default button for one of the Panels on the page.
        /// </summary>
        protected void SetDefaultButton(Panel panel, IButtonControl button)
        {
            if (button == null)
                throw new ArgumentNullException("button");
            if (!(button is Control))
                throw new ArgumentException("The button is not a Control.", "button");

            SetDefaultButton(panel, ((Control)button).UniqueID);
        }

        /// <summary>
        /// Sets the default button for one of the Panels on the page.
        /// </summary>
        protected void SetDefaultButton(Panel panel, string uniqueId)
        {
            if (panel == null)
                throw new ArgumentNullException("panel");

            // The documentation doesn't explain this properly, but the ID set here must be relative
            // to the panel ID.

            panel.DefaultButton = FieldInputHelper.GetRelativeId(uniqueId, panel, IdSeparator);
        }

        public bool HasError()
        {
            return ValidationSummary.HasError();
        }

        public void AddError(UserException ex)
        {
            AddError(ex.Message);
        }

        public void AddError(string message)
        {
            AddError(message, true);
        }

	    public void AddError(string message, bool htmlEncode)
		{
            ValidationSummary.AddError(message, htmlEncode);
		}

        public void AddInfo(string message)
        {
            AddInfo(message, true);
        }

        public void AddInfo(string message, bool htmlEncode)
        {
            ValidationSummary.AddInfo(message, htmlEncode);
        }

        public void AddConfirm(string message)
        {
            AddConfirm(message, true);
        }

        public void AddConfirm(string message, bool htmlEncode)
        {
            ValidationSummary.AddConfirm(message, htmlEncode);
        }

        public void DisablePageCaching()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.CacheControl = "no-cache";
        }

		protected void SetFocusOnControl(Control control)
		{
			var scriptStr = string.Format(
				"<script language=\"javascript\">document.getElementById(\"{0}\").focus();</script>",
				control.ClientID);
            ClientScript.RegisterStartupScript(typeof(LinkMePage), "Focus", scriptStr);
		}

		protected void ValidateTextLength(string value, TextBox textbox, string description)
		{
			if (value != null && textbox.MaxLength > 0 && value.Length > textbox.MaxLength)
			{
				AddError(string.Format("The {0} must not be longer than {1} characters.",
					description, textbox.MaxLength));
			}
		}

        public ReadOnlyUrl SetNotification(ReadOnlyUrl url, NotificationType notificationType, string message)
        {
            return Session.SetNotification(url, notificationType, message);
        }

        public void RedirectWithNotification(ReadOnlyUrl url, NotificationType notificationType, string message)
		{
            NavigationManager.Redirect(SetNotification(url, notificationType, message));
		}

        /// <summary>
        /// Retrieves a value from either Request.QueryString or Request.Form, but not server variables
        /// or cookies (like Request.Params does).
        /// </summary>
        protected string GetQueryOrFormValue(string name)
        {
            var request = Request;

            var value = request.QueryString[name] ?? request.Form[name];

            return value;
        }

#if DEBUG

        internal bool ContainsControlOfType<T>()
            where T : Control
        {
            return ContainsControlOfType<T>(Controls);
        }

        private static bool ContainsControlOfType<T>(IEnumerable controls)
            where T : Control
        {
            if (controls == null)
                return false;

            foreach (Control control in controls)
            {
                if (control is T)
                    return true;

                if (ContainsControlOfType<T>(control.Controls))
                    return true;
            }

            return false;
        }

#endif

		private void DisplayMessage()
		{
		    var notification = Session.GetNotification(GetClientUrl());
            if (notification == null)
				return;

            // Messages are HTML-encoded inside FeedbackMessage if necesssary, so don't encode again here.

            switch (notification.Type)
            {
                case NotificationType.Error:
                    AddError(notification.Message, false);
                    break;

                case NotificationType.Confirmation:
                    AddConfirm(notification.Message, false);
                    break;

                case NotificationType.Information:
                    AddInfo(notification.Message, false);
                    break;
            }

            Session.RemoveNotification(notification.Id);
		}

        private void CheckAuthorization()
        {
            var userTypes = AuthorizedUserTypes;
            if (userTypes != null)
            {
                if (userTypes.Length == 0)
                    EnsureNotAuthorized();
                else
                    EnsureAuthorized(userTypes);
            }
        }

        private void EnsureNotAuthorized()
        {
            var userId = LoggedInUserId;
            if (userId != null)
            {
                var redirectUrl = GetAuthorizedUrl();
                NavigationManager.Redirect(redirectUrl ?? HttpContext.Current.GetReturnUrl());
            }
        }

        protected virtual ReadOnlyUrl GetAuthorizedUrl()
        {
            return null;
        }

        private void EnsureAuthorized(IList<UserType> userTypes)
        {
            // Check that the current user matches the role.

            var userId = LoggedInUserId;
            if (userId == null)
            {
                // Redirect to the appropriate log in page for the first role.

                var userType = userTypes.Count == 0 ? UserType.Member : userTypes[0];
                if (userType == UserType.Employer)
                    NavigationManager.Redirect(HttpContext.Current.GetEmployerLoginUrl());
                NavigationManager.Redirect(HttpContext.Current.GetLoginUrl());
            }
            else
            {
                // If e.g. an employer is trying to access a member page, then redirect them home.

                if (!userTypes.Contains(LoggedInUserType))
                    NavigationManager.Redirect(HttpContext.Current.GetHomeUrl());
            }
        }

        private void CheckActivation()
        {
            if (RequiresActivation && LoggedInUserType == UserType.Member && !LoggedInUserActivated)
            {
                // Check whether the status needs to be updated.

                if (!LoggedInUserCheckStatus)
                    RedirectWithReturnUrlAndMessage(AccountsRoutes.NotActivated.GenerateUrl());

                // Reset.

                var user = _usersQuery.GetUser(LoggedInUserId.Value);
                _authenticationManager.UpdateUser(new HttpContextWrapper(HttpContext.Current), user, false);

                // Retry.

                if (!user.IsActivated)
                    RedirectWithReturnUrlAndMessage(AccountsRoutes.NotActivated.GenerateUrl());
            }
        }

        public UserType ActiveUserType
        {
            get
            {
                // If the current user is set then use that.

                var user = LoggedInUser;
                return user != null ? user.UserType : GetActiveUserType();
            }
        }

        private bool RequiresActivation
        {
            get
            {
                // If not a logged in member then does not require activation.

                var user = LoggedInUser;
                if (!(user is Member))
                    return false;
                return GetRequiresActivation();
            }
        }

        protected abstract UserType[] AuthorizedUserTypes { get; }
        protected abstract bool GetRequiresActivation();
        protected abstract UserType GetActiveUserType();

        private void RedirectWithReturnUrlAndMessage(ReadOnlyUrl url)
        {
            NavigationManager.Redirect(
                url,
                Apps.Asp.Constants.ReturnUrlParameter, Request.Url.AbsoluteUri,
                NotificationsExtensions.NotificationIdParameter, Request.QueryString[NotificationsExtensions.NotificationIdParameter]);
        }

        private void UpdatePageHeader()
        {
            // Set page details based on the current site map node.

            var node = CurrentSiteMapNode;
            if (node != null)
            {
                // Set the title.

                var title = node.GetActiveTitle(LoggedInUserType);
                if (!string.IsNullOrEmpty(title) && Page.Header != null)
                    SetTitle(title);

                // Add keywords and description meta tags.

                var keywords = node.Keywords;
                if (!string.IsNullOrEmpty(keywords))
                    AddMetaTag("keywords", keywords);
                var description = node.Description;
                if (!string.IsNullOrEmpty(description))
                    AddMetaTag("description", description);

                var noIndex = node.NoIndex;
                if (noIndex)
                    AddMetaTag("robots", "noindex");
            }
        }

	    public void AddNonFormContent(Control control)
	    {
            NonFormPlaceHolder.Controls.Add(control);
	    }

        public string FormContainerID
        {
            get { return FormContainer.ID; }
            set { FormContainer.ID = value; }
        }

        public string FormContainerCssClass
        {
            get { return FormContainer.CssClass; }
            set { FormContainer.CssClass = value; }
        }

        public NavigationSiteMapNode CurrentSiteMapNode
        {
            get { return _currentSiteMapNode ?? (_currentSiteMapNode = NavigationSiteMap.CurrentNode); }
        }

        protected new ILinkMeMasterPage Master
        {
            get { return base.Master as ILinkMeMasterPage; }
        }

        /// <summary>
        /// Returns the validation summary to use for displaying messages on the page or throws if one cannot be found.
        /// Must not return null.
        /// </summary>
        protected LinkMeValidationSummary ValidationSummary
        {
            get
            {
                var masterPage = Master;
                var summary = masterPage.ValidationSummary;
                if (summary == null)
                    throw new ApplicationException("The " + masterPage.GetType().Name + " master page does not have a validation summary.");

                return summary;
            }
        }
        
        protected SideBarContainer SideBarContainer
        {
            get
            {
                var masterPage = Master;
                var sideBarContainer = masterPage.SideBarContainer;
                if (sideBarContainer == null)
                    throw new ApplicationException("The " + masterPage.GetType().Name + " master page does not have a side bar container.");
                return sideBarContainer;
            }
        }

        private HtmlForm MainForm
        {
            get
            {
                var masterPage = Master;
                var form = masterPage.MainForm;
                if (form == null)
                    throw new ApplicationException("The " + masterPage.GetType().Name + " master page does not have a main form.");
                return form;
            }
        }

        private PlaceHolder NonFormPlaceHolder
        {
            get
            {
                var masterPage = Master;
                var placeHolder = masterPage.NonFormPlaceHolder;
                if (placeHolder == null)
                    throw new ApplicationException("The " + masterPage.GetType().Name + " master page does not have a non-form place holder.");
                return placeHolder;
            }
        }

        private ExplicitClientIdHtmlControl FormContainer
        {
            get
            {
                var masterPage = Master;
                var control = masterPage.FormContainer;
                if (control == null)
                    throw new ApplicationException("The " + masterPage.GetType().Name + " master page does not have a form container.");
                return control;
            }
        }

        public override void OnLoginAuthenticated()
        {
            var member = LoggedInMember;
            if (member != null)
            {
                SetUserContext(member);
                return;
            }

            var employer = LoggedInEmployer;
            if (employer != null)
            {
                // There is one special case that remains from previous work: Autopeople.

                var partner = _partnersQuery.GetPartner(employer.Id);
                if (partner != null)
                {
                    // Set the context.

                    var community = _communitiesQuery.GetCommunity(partner.Name);
                    if (community != null)
                    {
                        var vertical = _verticalsQuery.GetVertical(community.Id);
                        if (vertical != null)
                        {
                            ActivityContext.Current.Set(vertical);
                            return;
                        }
                    }
                }

                SetEmployerContext(employer);
                return;
            }

            var custodian = LoggedInCustodian;
            if (custodian != null)
            {
                SetUserContext(custodian);
            }
        }

        private static void SetEmployerContext(IEmployer employer)
        {
            // Only try to set the current community if it hasn't already been set.
            // This ensures that if a user comes in through a community channel then
            // the web site remains within that community channel, even if the user
            // is associated with a different community.

            var communityContext = ActivityContext.Current.Community;
            if (communityContext.IsSet)
                return;

            // Check whether the employer has a community.
            // If so, then set the request context to it.

            var affiliateId = employer.Organisation.AffiliateId;
            if (affiliateId == null)
                return;

            // The community must support it.

            var community = _communitiesQuery.GetCommunity(affiliateId.Value);
            if (community == null || !community.OrganisationsAreBranded)
                return;

            var vertical = _verticalsQuery.GetVertical(affiliateId.Value);
            if (vertical != null)
                ActivityContext.Current.Set(vertical);
        }

        private static void SetUserContext(IRegisteredUser member)
        {
            // Only try to set the current community if it hasn't already been set.
            // This ensures that if a user comes in through a community channel then
            // the web site remains within that community channel, even if the user
            // is associated with a different community.

            var communityContext = ActivityContext.Current.Community;
            if (communityContext.IsSet)
                return;

            // Check whether the member has a primary community.
            // If so, then set the request context to it.

            var affiliateId = member.AffiliateId;
            if (affiliateId == null)
                return;

            var vertical = _verticalsQuery.GetVertical(affiliateId.Value);
            if (vertical != null)
                ActivityContext.Current.Set(vertical);
        }
	}
}
