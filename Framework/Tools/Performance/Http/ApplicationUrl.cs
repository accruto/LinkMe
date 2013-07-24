using System;
using System.Collections.Specialized;

namespace LinkMe.Framework.Tools.Performance.Http
{
    public class ApplicationUrl
        : Url
    {
        private bool _isAbsolute;
        private readonly Application _application;

        #region Application

        public ApplicationUrl(Application application, bool? secure, string url, NameValueCollection queryString, string fragment)
            : base(application.GetAbsoluteUrl(url))
        {
            _application = application;
            SetAbsolute(url);
            SetSecure(secure);
            if (queryString != null)
                QueryString.Add(queryString);
            Fragment = fragment;
        }

        public ApplicationUrl(Application application, bool? secure, string url, NameValueCollection queryString)
            : this(application, secure, url, queryString, null)
        {
        }

        public ApplicationUrl(Application application, string url, NameValueCollection queryString, string fragment)
            : this(application, null, url, queryString, fragment)
        {
        }

        public ApplicationUrl(Application application, string url, NameValueCollection queryString)
            : this(application, null, url, queryString, null)
        {
        }

        public ApplicationUrl(Application application, bool? secure, string url)
            : base(application.GetAbsoluteUrl(url))
        {
            _application = application;
            SetAbsolute(url);
            SetSecure(secure);
        }

        #endregion

        public string AppRelativePath
        {
            get
            {
                string path = Path;
                string applicationPath = _application.ApplicationPath;

                // If the application is located at the root of IIS then just return the path.

                if (applicationPath.Length == 1 && applicationPath[0] == PathSeparatorChar)
                    return path;

                if (path.StartsWith(applicationPath, StringComparison.InvariantCultureIgnoreCase))
                    path = path.Substring(applicationPath.Length);
                return path;
            }
        }

        public string AppRelativePathAndQuery
        {
            get
            {
                string pathAndQuery = PathAndQuery;
                string applicationPath = _application.ApplicationPath;

                // If the application is located at the root of IIS then just return the path.

                if (applicationPath.Length == 1 && applicationPath[0] == PathSeparatorChar)
                    return pathAndQuery;

                if (pathAndQuery.StartsWith(applicationPath, StringComparison.InvariantCultureIgnoreCase))
                    pathAndQuery = pathAndQuery.Substring(applicationPath.Length);
                return pathAndQuery;
            }
        }

        public bool IsAbsolute
        {
            get { return _isAbsolute; }
        }

        protected override string GetHost()
        {
            return _isAbsolute ? base.GetHost() : _application.Host;
        }

        protected override void SetHost(string host)
        {
            base.SetHost(host);

            // If the host is explicitly set then the url is absolute.

            _isAbsolute = true;
        }

        private void SetSecure(bool? secure)
        {
            if (secure.HasValue)
            {
                _isAbsolute = true;

                if (secure.Value)
                {
                    Scheme = SecureScheme;
                    if (Port == SecurePort)
                        Port = -1;
                }
                else
                {
                    Scheme = InsecureScheme;
                    if (Port == InsecurePort)
                        Port = -1;
                }
            }
        }

        private void SetAbsolute(string url)
        {
            // If this is just a path then add in the scheme and host.

            if (url.Length > 0 && (url[0] == PathSeparatorChar || url[0] == Application.ApplicationPathStartChar))
                _isAbsolute = false;
            else
                _isAbsolute = true;
        }
    }
}
