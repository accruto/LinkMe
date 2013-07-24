using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Utility.Configuration;

namespace LinkMe.Web.Views.Shared
{
    /// <summary>
    /// Provides the JavaScript for Google Analytics tracking. The uacct value varies depending on the
    /// environment and a user-defined variable is set to the role of the logged in user, so it can be used
    /// in filters.
    /// </summary>
    public class GoogleAnalyticsTracker
        : TrackerUserControl
    {
        protected static readonly string UAcct = ApplicationContext.Instance.GetProperty(ApplicationContext.GOOGLE_ANALYTICS_UACCT);

        protected string UserRole
        {
            get
            {
                var roles = Context.User.UserType();
                return roles == UserType.Anonymous ? "Guest" : roles.ToString();
            }
        }
    }
}