using System;
using System.Net;
using System.Web;

namespace LinkMe.Framework.Utility.Urls
{
    public class Application
    {
        public const char ApplicationPathStartChar = '~';

        private static Application _current = new HttpContextApplication();
        private readonly string _host;
        private readonly int _port;
        private readonly string _applicationPath;

        public Application(string host, int port, string applicationPath)
        {
            _host = string.IsNullOrEmpty(host) ? Dns.GetHostName().ToLower() : host.ToLower();
            _port = port;
            _applicationPath = applicationPath == null ? null : applicationPath.ToLower();
        }

        public static Application Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public string GetAbsoluteUrl(string url)
        {
            // If this is just a path then add in the scheme, host, port and application path.

            if (url.Length > 0)
            {
                switch (url[0])
                {
                    case Url.PathSeparatorChar:
                        return Root.AddUrlSegments(url);

                    case ApplicationPathStartChar:
                        return ApplicationRoot.AddUrlSegments(url.Substring(1));
                }
            }

            return url;
        }

        public virtual string Host
        {
            get { return _host; }
        }

        public virtual string ApplicationPath
        {
            get
            {
                // Check that it is set to something.

                if (string.IsNullOrEmpty(_applicationPath))
                {
                    throw new InvalidOperationException("ApplicationPath has not been set. If this happens"
                                                        + " in a test make sure you call ApplicationSetUp.SetCurrentApplication() early on in"
                                                        + " a fixture setup. Do not call NavigationManager in test fixture field initialisers"
                                                        + " - call them in FixtureSetUp() instead.");
                }

                return _applicationPath;
            }
        }

        public virtual string Scheme
        {
            get { return Url.InsecureScheme; }
        }

        protected virtual int Port
        {
            get { return _port; }
        }

        private string Root
        {
            get
            {
                var port = Port;
                return Scheme + "://" + Host + (port == -1 ? string.Empty : ":" + port);
            }
        }

        private string ApplicationRoot
        {
            get { return Root.AddUrlSegments(ApplicationPath); }
        }
    }

    public class HttpContextApplication
        : Application
    {
        public HttpContextApplication()
            : base(string.Empty, -1, string.Empty)
        {
        }

        public HttpContextApplication(string host, string applicationPath)
            : base(host, -1, applicationPath)
        {
        }

        public override string Host
        {
            get { return HttpContext.Current != null ? HttpContext.Current.Request.Url.Host : base.Host; }
        }

        public override string ApplicationPath
        {
            get
            {
                // Try using the current request first.

                if (HttpContext.Current != null)
                    return HttpContext.Current.Request.ApplicationPath.ToLower();

                // Even though there may not be a current request we may still be executing inside ASP.NET so ask the runtime.

                var applicationPath = HttpRuntime.AppDomainAppVirtualPath;
                return !string.IsNullOrEmpty(applicationPath) ? applicationPath : base.ApplicationPath;
            }
        }

        public override string Scheme
        {
            get { return HttpContext.Current != null ? HttpContext.Current.Request.Url.Scheme : base.Scheme; }
        }

        protected override int Port
        {
            get { return HttpContext.Current != null ? HttpContext.Current.Request.Url.Port : base.Port; }
        }
    }
}