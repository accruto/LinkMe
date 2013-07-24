using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using LinkMe.Environment;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Agents.Applications
{
    public class WebSiteDetails
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _applicationPath;

        public WebSiteDetails(string host, int port, string applicationPath)
        {
            _host = host;
            _port = port;
            _applicationPath = applicationPath;
        }

        public string Host
        {
            get { return _host; }
        }

        public int Port
        {
            get { return _port; }
        }

        public string ApplicationPath
        {
            get { return _applicationPath; }
        }
    }

    public class ApplicationSetUp
    {
        private static IDictionary<WebSite, WebSiteDetails> _webSites = new Dictionary<WebSite, WebSiteDetails>();
        private static string _sourceRootPath;

        public static void SetSourceRootPath(string path)
        {
            _sourceRootPath = path;
        }

        public static void SetWebSites(IDictionary<WebSite, WebSiteDetails> webSites)
        {
            _webSites = webSites ?? new Dictionary<WebSite, WebSiteDetails>();
        }

        public static void SetCurrentApplication(WebSite webSite)
        {
            var host = GetHost(webSite, null);
            if (host == null)
                throw new ApplicationException(string.Format("Unable to get the host for the '{0}' web site.", webSite));
            var applicationPath = GetApplicationPath(webSite);
            if (applicationPath == null)
                throw new ApplicationException(string.Format("Unable to get the application path for the '{0}' web site because there is no IIS virtual directory configured.", webSite));
            Application.Current = new HttpContextApplication(host, applicationPath);
        }

        public static string GetHost(WebSite webSite, string host)
        {
            if (string.IsNullOrEmpty(host))
            {
                var value = GetHost(webSite);
                if (!string.IsNullOrEmpty(value))
                    return value;

                // Use the local host name.

                return Dns.GetHostName();
            }

            // Have a host corresponding to a vertical.
            // Need to potentially manipulate it for the environment.

            switch (RuntimeEnvironment.Environment)
            {
                case ApplicationEnvironment.Prod:
                    return host;

                case ApplicationEnvironment.Uat:
                    return RuntimeEnvironment.EnvironmentName + "." + host;

                case ApplicationEnvironment.Dev:
                    return "localhost." + host;

                default:
                    throw new ApplicationException("Unexpected current environment: " + RuntimeEnvironment.Environment);
            }
        }

        public static int GetPort(WebSite webSite)
        {
            var details = GetDetails(webSite);
            return details == null ? -1 : details.Port;
        }

        public static void RegisterPath()
        {
            try
            {
                if (RuntimeEnvironment.Environment == ApplicationEnvironment.Dev)
                {
                    // A hack. Simply write out the virtual path into the current app path.

                    var file = Path.Combine(HttpRuntime.AppDomainAppPath, "virtualpath.txt");
                    using (var writer = new StreamWriter(file))
                    {
                        writer.Write(HttpRuntime.AppDomainAppVirtualPath);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static string GetApplicationPath(WebSite webSite)
        {
            // In a dev environment the application path can be different from '/' so try to determine what it is.

            if (RuntimeEnvironment.Environment == ApplicationEnvironment.Dev || RuntimeEnvironment.Environment == ApplicationEnvironment.Uat)
            {
                // Keep the exception to help with problems.

                Exception exception = null;

                try
                {
                    var applicationPath = GetApplicationPathFromIis(webSite);
                    if (applicationPath != null)
                        return applicationPath;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                try
                {
                    var applicationPath = GetApplicationPathFromRegisteredPath(webSite);
                    if (applicationPath != null)
                        return applicationPath;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                // Use the default if in dev.

                if (RuntimeEnvironment.Environment == ApplicationEnvironment.Dev && exception != null)
                    return "/" + webSite;
//                    throw new ApplicationException("Cannot get the application path for the '" + webSite + "' web site.", exception);
            }

            // Default to simply '/'.

            var details = GetDetails(webSite);
            var path = details == null ? null : details.ApplicationPath; // ApplicationContext.Instance.GetProperty(GetWebSitePrefix(webSite) + ".applicationpath", true);
            return !string.IsNullOrEmpty(path) ? path : "/";
        }

        private static string GetApplicationPathFromIis(WebSite webSite)
        {
            // Need this to work out where the application is.

            if (string.IsNullOrEmpty(_sourceRootPath))
                return null;

            string sourceRootRelativePath;
            string buildPath;

            switch (webSite)
            {
                case WebSite.Management:
                    sourceRootRelativePath = "Apps\\Applications\\Management";
                    buildPath = "C:\\LinkMe\\Build\\Management";
                    break;

                case WebSite.Api:
                    sourceRootRelativePath = "Apps\\Applications\\Api";
                    buildPath = "C:\\LinkMe\\Build\\Api";
                    break;

                case WebSite.Integration:
                    sourceRootRelativePath = "Apps\\Applications\\Integration";
                    buildPath = "C:\\LinkMe\\Build\\Integration";
                    break;

                default:
                    sourceRootRelativePath = "Apps\\Web";
                    buildPath = "C:\\LinkMe\\Build\\Web";
                    break;
            }

            // Try what is passed in first but then also try the other location for the build server.

            var paths = new string[2];
            paths[0] = Path.Combine(_sourceRootPath, sourceRootRelativePath);
            paths[1] = buildPath;

            // Try each in turn.

            foreach (string path in paths)
            {
                string virtualPath = EnvironmentUtils.GetIisVirtualPathForPhysicalPath(path);
                if (virtualPath != null)
                    return virtualPath;
            }

            // If no path exists then nothing can be done.

            return null;
        }

        private static string GetApplicationPathFromRegisteredPath(WebSite webSite)
        {
            // Need this to work out where the application is.

            if (string.IsNullOrEmpty(_sourceRootPath))
                return null;

            if (RuntimeEnvironment.Environment == ApplicationEnvironment.Dev || RuntimeEnvironment.Environment == ApplicationEnvironment.Uat)
            {
                string sourceRootRelativePath;

                switch (webSite)
                {
                    case WebSite.Management:
                        sourceRootRelativePath = "Apps\\Management";
                        break;

                    case WebSite.Api:
                        sourceRootRelativePath = "Apps\\Api";
                        break;

                    case WebSite.Integration:
                        sourceRootRelativePath = "Apps\\Integration";
                        break;

                    default:
                        sourceRootRelativePath = "Apps\\Web";
                        break;
                }

                var path = Path.Combine(_sourceRootPath, sourceRootRelativePath);

                // Open the file.

                var file = Path.Combine(path, "virtualpath.txt");
                if (File.Exists(file))
                {
                    using (var reader = new StreamReader(file))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }

            return null;
        }

        private static string GetHost(WebSite webSite)
        {
            var details = GetDetails(webSite);
            return details == null ? null : details.Host;
        }

        private static WebSiteDetails GetDetails(WebSite webSite)
        {
            WebSiteDetails details;
            _webSites.TryGetValue(webSite, out details);
            return details;
        }
    }
}