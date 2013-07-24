using System.Web;
using LinkMe.Apps.Asp.Security;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Asp.Modules
{
	public class AuthenticationModule
        : HttpModule
	{
        private static IAuthenticationManager _authenticationManager;
        private static IAccountsManager _accountsManager;

        protected override void OnAuthenticateRequest()
        {
            var context = new HttpContextWrapper(HttpContext.Current);
            if (!context.Request.IsAuthenticated)
                GetAccountsManager().TryAutoLogIn(context);
            GetAuthenticationManager().AuthenticateRequest(context);
        }

        protected override void OnPostAuthenticateRequest()
        {
            var context = new HttpContextWrapper(HttpContext.Current);
            if (!context.Request.IsAuthenticated)
                GetAuthenticationManager().PostAuthenticateRequest(context);
        }

	    private static IAuthenticationManager GetAuthenticationManager()
        {
            if (_authenticationManager == null)
                _authenticationManager = Container.Current.Resolve<IAuthenticationManager>();
            return _authenticationManager;
        }

        private static IAccountsManager GetAccountsManager()
        {
            if (_accountsManager == null)
                _accountsManager = Container.Current.Resolve<IAccountsManager>();
            return _accountsManager;
        }
    }
}
