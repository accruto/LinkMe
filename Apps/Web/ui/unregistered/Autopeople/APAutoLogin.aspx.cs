using System;
using System.Web;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Users.Sessions;
using LinkMe.Apps.Agents.Users.Sessions.Commands;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Security;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Web.Areas.Employers.Routes;

namespace LinkMe.Web.UI.Unregistered.Autopeople
{
    public partial class APAutoLogin : LinkMePage
    {
        private const string TicketDecryptionKey = "asd123fgh";
        private const string VerticalName = "Autopeople";
        private const string UsernameParameter = "username";
        private const string PasswordParameter = "pwd";
        private const string TicketParameter = "ticket";

        private static readonly IUserSessionsCommand _userSessionsCommand = Container.Current.Resolve<IUserSessionsCommand>();
        private static readonly ILoginAuthenticationCommand _loginAuthenticationCommand = Container.Current.Resolve<ILoginAuthenticationCommand>();

        private IRegisteredUser _currentUser;

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

        private bool IsTicketPresent
        {
            get { return !String.IsNullOrEmpty(Request.Params[TicketParameter]); }
        }

        private string Username
        {
            get
            {
                if (IsTicketPresent)
                {
                    string [] contents = GetTicketContents();
                    if(contents == null)
                    {
                        return String.Empty;
                    }
                    return contents[0];
                }
                
                return GetParamString(UsernameParameter);
            }
        }

        private string Ticket
        {
            get { return Request.Params[TicketParameter]; }
        }

        private string Password
        {
            get
            {
                if (IsTicketPresent)
                {
                    string[] contents = GetTicketContents();
                    if (contents == null)
                    {
                        return String.Empty;
                    }
                    return contents[1];
                }
                
                return GetParamString(PasswordParameter);
            }
        }

        private string[] GetTicketContents()
        {
            string decrTicket = RC4.Decrypt(TicketDecryptionKey, RC4.hex2bin("", Ticket), false);
            string[] contents = decrTicket.Split(';');
            if (contents.Length != 2)
            {
                return null;
            }
            return contents;
        }

        private string GetParamString(string paramName)
        {
            string srt = Request.QueryString[paramName];
            if (!String.IsNullOrEmpty(srt))
                return srt;
            return string.Empty;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var status = AuthenticationStatus.Failed;

            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
            {
                var result = _loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = Username, Password = Password });
                _currentUser = result.User;
                status = result.Status;
                    
                if (result.User != null)
                {
                    switch (result.Status)
                    {
                        case AuthenticationStatus.Authenticated:
                        case AuthenticationStatus.AuthenticatedMustChangePassword:
                        case AuthenticationStatus.AuthenticatedWithOverridePassword:
                        case AuthenticationStatus.Deactivated:
                            _authenticationManager.LogIn(new HttpContextWrapper(HttpContext.Current), _currentUser, AuthenticationStatus.Authenticated);
                            break;

                        default:
                            _currentUser = null;
                            break;
                    }
                }
            } 

            if (_currentUser != null)
            {
                _userSessionsCommand.CreateUserLogin(new UserLogin {UserId = _currentUser.Id, IpAddress = Request.UserHostAddress, AuthenticationStatus = status});

                // This specific page is like a vertical landing page, so set the context.

                var vertical = _verticalsQuery.GetVertical(VerticalName);
                if (vertical != null)
                    ActivityContext.Current.Set(vertical);

                // Redirect to the appropriate page.

                ReadOnlyUrl referrer = null;
                var refParameter = Request.QueryString["ref"];
                if (refParameter != null)
                    referrer = new ReadOnlyApplicationUrl(refParameter);
                NavigationManager.Redirect(referrer ?? SearchRoutes.Search.GenerateUrl());
            }
        }
    }
}
