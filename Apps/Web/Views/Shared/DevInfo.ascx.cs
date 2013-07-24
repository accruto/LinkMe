using System.Data.SqlClient;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Content;

namespace LinkMe.Web.Views.Shared
{
    public class DevInfo
        : ViewUserControl
    {
        private readonly IDbConnectionFactory _connectionFactory = Container.Current.Resolve<IDbConnectionFactory>();

        private static readonly ReadOnlyUrl _devUrl = GetUrlForPage<dev>(dev.ActionParam, dev.ActionInfo);

        protected static ReadOnlyUrl FireBugUrl
        {
            get { return JavaScripts.FireBug.Url; }
        }

        protected static ReadOnlyUrl DevUrl
        {
            get { return _devUrl; }
        }

        protected static bool ShowAuthenticationInfo
        {
            get { return ApplicationContext.Instance.GetBoolProperty(ApplicationContext.SHOW_AUTHENTICATION_INFO); }
        }

        protected string Database
        {
            get
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    var text = connection.Database;
                    var sqlConnection = connection as SqlConnection;
                    if (sqlConnection != null)
                        text += " on " + sqlConnection.DataSource;
                    return text;
                }
            }
        }

        protected string Identity
        {
            get
            {
                var principal = Context.User;
                if (principal != null)
                {
                    var identity = principal.Identity;
                    if (identity != null && !string.IsNullOrEmpty(identity.Name))
                        return identity.Name;
                }
                return "None";
            }
        }
    }
}