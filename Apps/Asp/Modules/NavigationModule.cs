using System.Web;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Asp.Modules
{
    public class NavigationModule
        : HttpModule
    {
        private static readonly EventSource EventSource = new EventSource<NavigationModule>();

        protected override void OnBeginRequest()
        {
            const string method = "OnBeginRequest";

            var clientUrl = GetClientUrl();

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Beginning request for url: " + clientUrl, Event.Arg("clientUrl", clientUrl), Event.Arg("method", HttpContext.Current.Request.HttpMethod));

            var path = clientUrl.Path;

            // Avoid error messages caused by VS.NET's request for a non-existent file.

            if (!path.EndsWith("get_aspx_ver.aspx"))
            {
                if (!NavigationManager.IsExcluded(path))
                    NavigationManager.CheckRequest(clientUrl, HttpContext.Current.Request.HttpMethod);
            }

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Request begun for url: " + clientUrl, Event.Arg("clientUrl", clientUrl));
        }

        protected override void OnPreRequestHandlerExecute()
        {
            const string method = "OnPreRequestHandlerExecute";

            base.OnPreRequestHandlerExecute();

            // Track the request here.

            if (EventSource.IsEnabled(Event.RequestTracking))
            {
                // At the moment only tracking logged in employers.

                var user = HttpContext.Current.User as RegisteredUserPrincipal;
                if (user != null)
                {
                    var identity = user.Identity as RegisteredUserIdentity;
                    if (identity != null && identity.UserType == UserType.Employer)
                    {
                        EventSource.Raise(
                            Event.RequestTracking,
                            method,
                            "Tracking request: " + GetClientUrl().AbsoluteUri);
                    }
                }
            }
        }
    }
}
