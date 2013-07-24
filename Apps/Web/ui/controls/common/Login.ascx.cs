using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Validation;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Content;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Framework.Instrumentation;
using AccountsRoutes=LinkMe.Web.Areas.Accounts.Routes.AccountsRoutes;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class Login : LinkMeUserControl
    {
        private static readonly ILoginCredentialsQuery _loginCredentialsQuery = Container.Current.Resolve<ILoginCredentialsQuery>();
        private static readonly ILoginAuthenticationCommand _loginAuthenticationCommand = Container.Current.Resolve<ILoginAuthenticationCommand>();
        private static readonly IDevAuthenticationManager _devAuthenticationManager = Container.Current.Resolve<IDevAuthenticationManager>();
        private static readonly ICookieManager _cookieManager = Container.Current.Resolve<ICookieManager>();
        private static readonly IFaqsQuery _faqsQuery = Container.Current.Resolve<IFaqsQuery>();

        private static readonly Guid DisableFaqId = new Guid("7B7FAD42-E027-4586-843B-4D422F39E7EA");

        public const string UserIdParameter = "userId";
        public const string UserIdKnownParameter = "userIdKnown";

        public const string LoginFormValidationGroup = "loginValGroup";

        // EP: make sure it's the right user ID textbox - there can be multiple.
        // AS: this setTimeout here is required to get arounf FF throwing an exception when trying
        // programmaticaly to move focus away from edit box with dropdown suggestion visible.
        private const string UserIdKeyPressScript = @"
	function userIdKeyPress(e, toFocusId)
	{
		keycode = window.event ? e.keyCode : e.which ? e.which : null;
		if (keycode != 13)
			return true;

		setTimeout(function() { $(toFocusId).focus(); }, 100);		
		return false;
	}
";

        private static readonly EventSource EventSource = new EventSource(typeof(Login));

        private static ReadOnlyUrl _memberJoinUrl { get { return JoinRoutes.Join.GenerateUrl(); } }
        private static ReadOnlyUrl _employerJoinUrl { get { return Areas.Employers.Routes.AccountsRoutes.Join.GenerateUrl(); } }
        private static ReadOnlyUrl _memberRequestNewPasswordUrl { get { return AccountsRoutes.NewPassword.GenerateUrl(new { userType = UserType.Member }); } }
        private static ReadOnlyUrl _employerRequestNewPasswordUrl { get { return AccountsRoutes.NewPassword.GenerateUrl(new { userType = UserType.Employer }); } }
        
        private IRegisteredUser _currentUser;
        private bool _isMemberRole = true;
        private ReadOnlyUrl _actionUrl;
        private bool _grabFocusOnLoad = true;

        public string LoginButtonId
        {
            // Contrary to the documentation this must be the UniqueID, not the ID
            get { return btnLogin.UniqueID; }
        }

        public bool ShowJoinLinks
        {
            get { return phJoin.Visible; }
            set { phJoin.Visible = value; }
        }

        // A user ID to pre-populate the input with, but only if "Remember Me" doesn't have a username set
        // (ie. "Remember Me" takes precedence).
        public string PrepopulatedUserId { get; set; }

        public bool IsMemberRole
        {
            get { return _isMemberRole; }
            set { _isMemberRole = value; }
        }

        protected static ReadOnlyUrl MemberJoinUrl
        {
            get { return _memberJoinUrl; }
        }

        protected static ReadOnlyUrl EmployerJoinUrl
        {
            get { return _employerJoinUrl; }
        }

        public ReadOnlyUrl ActionUrl
        {
            get
            {
                return _actionUrl;
            }
            set
            {
                _actionUrl = value;
                frmLogin.Action = _actionUrl == null ? null : _actionUrl.ToString();
            }
        }

        protected ReadOnlyUrl ForgotPasswordUrl
        {
            get { return !_isMemberRole ? _employerRequestNewPasswordUrl : _memberRequestNewPasswordUrl; }
        }

        public string UserIdClientId
        {
            get { return txtUserId.ClientID; }
        }

        public string PasswordClientId
        {
            get { return txtPassword.ClientID; }
        }

        private string UserId
        {
            get { return (frmLogin.IsPostBack ? Request.Form[txtUserId.UniqueID] : txtUserId.Text); }
            set { txtUserId.Text = value; }
        }

        private string Password
        {
            get { return (frmLogin.IsPostBack ? Request.Form[txtPassword.UniqueID] : txtPassword.Text); }
            set { txtPassword.Text = value; }
        }

        private bool LoginPersist
        {
            get { return (frmLogin.IsPostBack ? Request.Form[chkLoginPersist.UniqueID] == "on" : chkLoginPersist.Checked); }
            set { chkLoginPersist.Checked = value; }
        }

        protected string FocusScript { get; private set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Login);

            RegisterUserIdKeyPressScript(Page);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtUserId.Attributes["onKeyPress"] = string.Format("return userIdKeyPress(event, '{0}');",
                txtPassword.ClientID);

            if (ApplicationContext.Instance.GetBoolProperty(ApplicationContext.SSL_REDIRECT_LOGINS))
            {
                Url url;
                if (string.IsNullOrEmpty(frmLogin.Action))
                {
                    url = GetClientUrl().AsNonReadOnly();
                    url.Scheme = Url.SecureScheme;
                }
                else
                {
                    url = new ApplicationUrl(true, frmLogin.Action);
                }

                frmLogin.Action = url.ToString();
            }

            if (frmLogin.IsPostBack)
            {
                UserId = UserId; // This looks like such a hack - and, yes, it is.
                ProcessLogin(false);
            }
            else
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    UserId = GetQueryStringUserId();

                    if (UserId.Length > 0 && Request.QueryString[UserIdKnownParameter] == bool.TrueString)
                    {
                        lblLoginMsg.Text = "Email recognised, enter password and log in.";
                        phLoginMsg.Visible = true;
                    }

                    if (PrepopulateFields()) // remember me sets the user & password textboxes
                    {
                        ProcessLogin(true);
                    }
                }

                if (GrabFocusOnLoad)
                {
                    SetFocusOnControl(string.IsNullOrEmpty(UserId) ? txtUserId : txtPassword);
                }
            }
        }

        private string GetQueryStringUserId()
        {
            var userId = Request.QueryString[UserIdParameter];
            if (userId == null)
                return null;

            // Try to convert to a guid.

            try
            {
                var guid = new Guid(userId);
                return _loginCredentialsQuery.GetLoginId(guid);
            }
            catch (Exception)
            {
            }

            return userId;
        }

        public static void RegisterUserIdKeyPressScript(Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(typeof(Login), "userIdKeyPress",
                UserIdKeyPressScript, true);
        }

        public bool GrabFocusOnLoad
        {
            get { return _grabFocusOnLoad; }
            set { _grabFocusOnLoad = value; }
        }

        // Can't use the base SetFocusOnControl(), because that adds the script to the main form, which is above
        // the control we want to focus on.
        protected override void SetFocusOnControl(Control toFocus)
        {
            FocusScript = "<script language=\"javascript\">" + GetSetFocusScript(toFocus) + "</script>";
        }

        public void ProcessLogin(bool usedRememberMe)
        {
            const string method = "ProcessLogin";

            Page.Validate(LoginFormValidationGroup);
            if (!Page.IsValid)
                return;

            string userId = UserId;
            string password = Password;

            if (userId.Length == 0 || password.Length == 0)
            {
                lblLoginMsg.Text = ValidationErrorMessages.LOGIN_ENTER_DATA;
                phLoginMsg.Visible = true;
                return;
            }

            var result = _loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = userId, Password = password });
            _currentUser = result.User;

            if (result.User != null)
            {
                switch (result.Status)
                {
                    case AuthenticationStatus.AuthenticatedWithOverridePassword:
                        // Authenticated with the override password, so give them access to developer
                        // features as well (like viewing exception details).

                        _devAuthenticationManager.LogIn(HttpContext.Current);
                        goto case AuthenticationStatus.Authenticated;

                    case AuthenticationStatus.AuthenticatedMustChangePassword:
                        CompleteAuthenticatedLogin(result.Status, true);
                        break;

                    case AuthenticationStatus.Authenticated:
                        CompleteAuthenticatedLogin(result.Status, false);
                        break;

                    case AuthenticationStatus.Disabled:

                        CompleteDisabledLogin();
                        break;

                    case AuthenticationStatus.Deactivated:

                        // Employers and administrators should not be affected by this flag so try to let them through.

                        if (_currentUser is Employer || _currentUser is Administrator)
                            CompleteAuthenticatedLogin(result.Status, false);
                        else
                            CompleteDeactivatedLogin(result.Status);
                        break;
                }
            }

            if (result.Status == AuthenticationStatus.Failed)
            {
                EventSource.Raise(Event.Trace, method, string.Format("User login has failed. LoginId = '{0}'", userId));
                lblLoginMsg.Text = ValidationErrorMessages.LOGIN_FAILED_ONE_LINE;
                phLoginMsg.Visible = true;

                // POST requests from external forms will not populate txtUserId.
                // This ensures it's populated when we bounce users after a failure.
                if (Request.RequestType == "POST" && !IsPostBack)
                    txtUserId.Text = UserId;

                SetFocusOnControl(txtPassword);
            }
        }

        private bool PrepopulateFields()
        {
            // There are two ways to prepopulate the user id.

            // First way, because another page has indicated that it knows who the user is by setting the context.

            var requestUserId = AnonymousUserContext.RequestUserId;
            if (requestUserId != null)
            {
                var credentials = _loginCredentialsQuery.GetCredentials(requestUserId.Value);
                if (credentials != null)
                {
                    AnonymousUserContext.RequestUserId = null;
                    UserId = credentials.LoginId;
                    return false;
                }
            }

            // Second way, because the user has set up Remember Me.

            var persistantCredentials = _cookieManager.ParsePersistantUserCookie(new HttpContextWrapper(HttpContext.Current));
            if (!string.IsNullOrEmpty(persistantCredentials.LoginId))
            {
                UserId = persistantCredentials.LoginId;
                LoginPersist = true;

                if (!string.IsNullOrEmpty(persistantCredentials.Password))
                {
                    Password = persistantCredentials.Password;
                    return true;
                }
            }
            else if (!string.IsNullOrEmpty(PrepopulatedUserId))
            {
                UserId = PrepopulatedUserId;
            }

            return false;
        }

        public void Populate(string loginId, string password, bool remember)
        {
            UserId = loginId;
            Password = password;
            LoginPersist = remember;
        }

        private void RedirectUser(bool mustChangePassword)
        {	
            Debug.Assert(_currentUser != null, "currentUser != null");

            var url = WebUtils.GetLoginRedirectUrl(_currentUser.Id.ToString("n"));
            CheckUserCredentialsConsistency(_currentUser.Id, url);

            if (mustChangePassword)
                NavigationManager.Redirect(AccountsRoutes.MustChangePassword.GenerateUrl(), Apps.Asp.Constants.ReturnUrlParameter, url.ToString());

            var member = _currentUser as Member;
            if (member != null && member.EmailAddresses.Any(a => !a.IsVerified))
                url = WebUtils.SetEmailBouncedNotification(url, Session);

            NavigationManager.Redirect(url);
        }

        private static void CheckUserCredentialsConsistency(Guid userUniqueId, ReadOnlyUrl redirectUrl)
        {
            const string method = "CheckUserCredentialsConsistency";

            if (new Guid(HttpContext.Current.User.Identity.Name) != userUniqueId)
            {
                var message = string.Format(@"User was logged out as identity does NOT match unique id from
													log in('{0}' != '{1}'); IP:{2} !! ", 
                    HttpContext.Current.User.Identity.Name,
                    userUniqueId,HttpContext.Current.Request.UserHostAddress);
                EventSource.Raise(Event.Warning, method, message);
                _authenticationManager.LogOut(new HttpContextWrapper(HttpContext.Current));
                NavigationManager.Redirect(NavigationManager.GetLogInUrl(), Apps.Asp.Constants.ReturnUrlParameter, redirectUrl.PathAndQuery);
            }
        }

        private void OnLoginAuthenticated(bool mustChangePassword)
        {
            // Always give the base page a chance to do something first.

            LinkMePage.OnLoginAuthenticated();
            RedirectUser(mustChangePassword);
        }

        private static void OnLoginDisabled()
        {
            var faq = _faqsQuery.GetFaq(DisableFaqId);
            NavigationManager.Redirect(faq.GenerateUrl(_faqsQuery.GetCategories()));
        }

        private static void OnLoginDeactivated()
        {
            NavigationManager.Redirect(AccountsRoutes.NotActivated.GenerateUrl());
        }

        private void CompleteAuthenticatedLogin(AuthenticationStatus status, bool mustChangePassword)
        {
            const string method = "CompleteAuthenticatedLogin";

            var context = HttpContext.Current;

            _authenticationManager.LogIn(new HttpContextWrapper(context), _currentUser, status);

            EventSource.Raise(Event.Trace, method, string.Format("User logged in with Id = '{0}', Role = {1}", _currentUser.Id, _currentUser.UserType));

            if (LoginPersist)
                _cookieManager.CreatePersistantUserCookie(new HttpContextWrapper(context), _currentUser.UserType, new LoginCredentials { LoginId = UserId, Password = Password }, status);
            else
                _cookieManager.DeletePersistantUserCookie(new HttpContextWrapper(context));

            OnLoginAuthenticated(mustChangePassword);
        }

        private void CompleteDisabledLogin()
        {
            _currentUser = null;
            OnLoginDisabled();
        }

        private void CompleteDeactivatedLogin(AuthenticationStatus status)
        {
            _authenticationManager.LogIn(new HttpContextWrapper(HttpContext.Current), _currentUser, status);
            OnLoginDeactivated();
        }
    }
}
