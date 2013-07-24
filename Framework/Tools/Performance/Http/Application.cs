using System;
using System.Net;

namespace LinkMe.Framework.Tools.Performance.Http
{
    public class Application
    {
        public const char ApplicationPathStartChar = '~';

        private readonly string _host;
        private readonly int _port;
        private readonly string _applicationPath;

        public Application(string host, int port, string applicationPath)
        {
            _host = string.IsNullOrEmpty(host) ? Dns.GetHostName().ToLower() : host.ToLower();
            _port = port;
            _applicationPath = applicationPath == null ? null : applicationPath.ToLower();
        }

        public string GetAbsoluteUrl(string url)
        {
            // If this is just a path then add in the scheme, host, port and application path.

            if (url.Length > 0)
            {
                switch (url[0])
                {
                    case Url.PathSeparatorChar:
                        return Url.Combine(Root, url);

                    case ApplicationPathStartChar:
                        return Url.Combine(ApplicationRoot, url.Substring(1));
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
                        + " a fixture setup.");
                }

                return _applicationPath;
            }
        }

        protected virtual string Scheme
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
            get { return Url.Combine(Root, ApplicationPath); }
        }
    }
}
