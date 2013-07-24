using System.Web;
using LinkMe.Apps.Asp.Modules;
using LinkMe.Apps.Asp.Security;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Api.Modules
{
    public class AuthenticationModule
        : HttpModule
    {
        private static IAuthenticationManager _authenticationManager;

        protected override void OnAuthenticateRequest()
        {
            GetAuthenticationManager().AuthenticateRequest(new HttpContextWrapper(HttpContext.Current));
        }

        private static IAuthenticationManager GetAuthenticationManager()
        {
            if (_authenticationManager == null)
                _authenticationManager = Container.Current.Resolve<IAuthenticationManager>();
            return _authenticationManager;
        }
    }
}