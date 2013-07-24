using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.UI;

namespace LinkMe.Web
{
    public partial class dev : LinkMePage
    {
        public const string ActionParam = "action";
        public const string ActionError = "error"; // Throw an unhandled exception.
        public const string ActionInfo = "info"; // Show some server information.
        public const string ActionShutdown = "shutdown"; // Shut down the ASP.NET AppDomain.
        public const string ActionConfig = "config"; // Show all server configuration.
        public const string ActionGetEnvironment = "getenvironment"; // Return the current environment - for tests
        public const string ErrorTextParam = "errorText";
        public const string CacheRegions = "regions";

        private static readonly IDbConnectionFactory _connectionFactory = Container.Current.Resolve<IDbConnectionFactory>();
        private static readonly ILoginCredentialsQuery _loginCredentialsQuery = Container.Current.Resolve<ILoginCredentialsQuery>();
        private static readonly IDevAuthenticationManager _devAuthenticationManager = Container.Current.Resolve<IDevAuthenticationManager>();

        private string _commandOutput;

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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            btnViewError.Click += btnViewError_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_devAuthenticationManager.IsLoggedIn(HttpContext.Current))
            {
                ProcessTestCommand();
                phAuthenticated.Visible = true;
            }
            else
            {
                phAuthRequired.Visible = true;
                SetFocusOnControl(txtPassword);
            }
        }

        protected string CommandOutput
        {
            get { return _commandOutput; }
        }

        private void ProcessTestCommand()
        {
            string command = Request.QueryString[ActionParam];

            switch (command)
            {
                case null:
                case "":
                    return;

                case ActionError:
                    // Throw an error for testing purposes.

                    string errorText = Request.QueryString[ErrorTextParam] ?? "Test error requested by user.";

                    throw new ApplicationException(errorText);

                case ActionInfo:
                    _commandOutput = GetServerInfoHtml();
                    break;

                case ActionConfig:
                    _commandOutput = GetAllConfiguration();
                    break;

                case ActionShutdown:
                    _commandOutput = ShutDown();
                    break;

                case ActionGetEnvironment:
                    WriteCurrentEnvironment();
                    break;

                default:
                    Response.Write("The command '" + command + "' is unrecognised.");
                    Response.End();
                    break;
            }
        }

        private void WriteCurrentEnvironment()
        {
            // Just write the environment name by itself. This is used by unit tests to check that
            // the ASP.NET process is also running in dev.

            Response.Clear();
            Response.ContentType = "text/plain";
            Response.Write(RuntimeEnvironment.EnvironmentName);
            Response.End();
        }

        private static string GetServerInfoHtml()
        {
            LoginCredentials credentials = null;
            var user = _authenticationManager.GetUser(new HttpContextWrapper(HttpContext.Current));
            if (user != null)
                credentials = _loginCredentialsQuery.GetCredentials(user.Id);
            string userId = (credentials == null ? "(none)" : credentials.LoginId + " (" + user.Id + ")");

            string lensServers = "";
            lensServers += string.Format("{0}:{1}<br />", Container.Current.Resolve<string>("lens.connection.host"), Container.Current.Resolve<int>("lens.connection.port"));

            string dbServer, dbName;
            using (IDbConnection conn = _connectionFactory.CreateConnection())
            {
                dbServer = (conn is SqlConnection ? ((SqlConnection)conn).DataSource : null);
                dbName = conn.Database;
            }

            string searchMeServer = ApplicationContext.Instance.GetProperty(ApplicationContext.SEARCHME_SERVER, true);
            if (string.IsNullOrEmpty(searchMeServer))
            {
                searchMeServer = "(LOCAL)";
            }

            return string.Format("<table><tr><td>Server name:</td><td>{0}</td></tr>"
                + "<tr><td>User ID:</td><td>{1}</td></tr>"
                + "<tr><td>LENS servers:</td><td>{2}</td></tr>"
                + "<tr><td>Database:</td><td>{3} on {4}</td></tr>"
                + "<tr><td>SearchMe server:</td><td>{5}</td></tr>"
                + "<tr><td>Environment:</td><td>{6}</td></tr>",
                System.Environment.MachineName, userId, lensServers,
                dbName, dbServer, searchMeServer, RuntimeEnvironment.EnvironmentName);
        }

        private static string GetAllConfiguration()
        {
            IDictionary<string, string> values = ApplicationContext.Instance.GetPropertyValues();
            IDictionary<string, string> sources = ApplicationContext.Instance.GetPropertySources();

            var sb = new StringBuilder();
            sb.Append("<table id='configTable'>");
            sb.Append("<tr><td class='configName'><b>Property</b></td><td><b>Value</b></td><td class='configSource'><b>Source</b></td></tr>");

            foreach (KeyValuePair<string, string> kvp in sources)
            {
                string settingName = kvp.Key;
                string settingValue = values[settingName];

                string cssClass = null;
                if (IsPassword(settingName, settingValue))
                {
                    cssClass = "configPassword";
                    settingValue = "*****";
                }
                else
                {
                    settingValue = DatabaseHelper.BlankOutConnectionStringPassword(settingValue);

                    if (kvp.Value.IndexOf("(embedded)") == -1)
                    {
                        cssClass = "configOverridden";
                    }
                }

                sb.AppendFormat("<tr{0}><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                    cssClass == null ? "" : " class='" + cssClass + "'", HtmlUtil.TextToHtml(settingName),
                    HtmlUtil.TextToHtml(settingValue), HtmlUtil.TextToHtml(kvp.Value));
            }

            sb.Append("</table>");
            return sb.ToString();
        }

        private static bool IsPassword(string name, string value)
        {
            const StringComparison comparison = StringComparison.InvariantCultureIgnoreCase;

            return (name.IndexOf("password", comparison) != -1
                && !string.Equals(value, bool.TrueString, comparison)
                && !string.Equals(value, bool.FalseString, comparison));
        }

        private static string ShutDown()
        {
            HttpRuntime.UnloadAppDomain();
            return "<h4>The application has been shut down. It will automatically restart on the next HTTP request.</h4>";
        }

        private void btnViewError_Click(object sender, EventArgs e)
        {
            if (_devAuthenticationManager.AuthenticateUser(txtPassword.Text) == AuthenticationStatus.Authenticated)
            {
                _devAuthenticationManager.LogIn(HttpContext.Current);
                ProcessTestCommand();
                phAuthenticated.Visible = true;
                phAuthRequired.Visible = false;
            }
            else
            {
                lblResponse.Text = "The password is incorrect.";
            }
        }
    }
}
