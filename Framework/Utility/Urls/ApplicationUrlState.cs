using System;
using System.Web;

namespace LinkMe.Framework.Utility.Urls
{
    internal class ApplicationUrlState
    {
        private bool _isAbsolute;
        private bool? _isSecure;
        private readonly Application _application;
        private readonly bool _isCurrentApplication;

        public ApplicationUrlState()
        {
            _application = Application.Current;
            _isCurrentApplication = true;
            _isAbsolute = false;
            _isSecure = null;
        }

        public ApplicationUrlState(Application application, string url)
        {
            _application = application;
            _isCurrentApplication = ReferenceEquals(application, Application.Current);
            _isAbsolute = GetIsAbsolute(url);
            _isSecure = null;
        }

        public ApplicationUrlState(Application application, ReadOnlyUrl url)
        {
            _application = application;
            _isCurrentApplication = ReferenceEquals(application, Application.Current);
            _isAbsolute = GetIsAbsolute(url);
            _isSecure = GetIsSecure(url);
        }

        public ApplicationUrlState(ReadOnlyUrl url)
            : this(Application.Current, url)
        {
        }

        private ApplicationUrlState(Application application, bool isCurrentApplication, bool isAbsolute, bool? isSecure)
        {
            _application = application;
            _isCurrentApplication = isCurrentApplication;
            _isAbsolute = isAbsolute;
            _isSecure = isSecure;
        }

        public bool IsAbsolute
        {
            get { return _isAbsolute; }
            set { _isAbsolute = value; }
        }

        public bool? IsSecure
        {
            get { return _isSecure; }
            set { _isSecure = value; }
        }

        public Application Application
        {
            get { return _application; }
        }

        public ApplicationUrlState Clone()
        {
            return new ApplicationUrlState(_application, _isCurrentApplication, _isAbsolute, _isSecure);
        }

        public string ToString(ReadOnlyUrl url)
        {
            // Use the current context.

            if (HttpContext.Current != null)
            {
                if (!_isAbsolute && _isSecure == null)
                    return url.PathAndQuery;

                var requestUri = HttpContext.Current.Request.Url;

                // If security has been explicitly set for a relative url then compare just that.

                if (!_isAbsolute && _isCurrentApplication)
                    return requestUri.Scheme == (_isSecure.Value ? Url.SecureScheme : Url.InsecureScheme) ? url.PathAndQuery : null;

                // If the scheme, host and port are the same then use a relative path.

                var scheme = url.Scheme;
                var host = url.Host;
                var port = url.Port;

                if (requestUri.Scheme == scheme
                    && string.Compare(requestUri.Host, host, true) == 0
                    && (requestUri.Port == port || port == -1))
                    return url.PathAndQuery;
            }

            // Cannot use the path only.

            return null;
        }

        public string GetAppRelativePath(string path)
        {
            var applicationPath = _application.ApplicationPath;

            // If the application is located at the root of IIS then just return the path.

            if (applicationPath.Length == 1 && applicationPath[0] == Url.PathSeparatorChar)
                return path;

            if (path.StartsWith(applicationPath, StringComparison.InvariantCultureIgnoreCase))
                path = path.Substring(applicationPath.Length);
            return path;
        }

        private static bool GetIsAbsolute(string url)
        {
            // If this is just a path not absolute.

            return url.Length <= 0 || (url[0] != Url.PathSeparatorChar && url[0] != Application.ApplicationPathStartChar);
        }

        private static bool GetIsAbsolute(ReadOnlyUrl url)
        {
            if (url is ApplicationUrl)
                return ((ApplicationUrl)url).IsAbsolute;
            else if (url is ReadOnlyApplicationUrl)
                return ((ReadOnlyApplicationUrl)url).IsAbsolute;
            else
                return true;
        }

        private static bool? GetIsSecure(ReadOnlyUrl url)
        {
            if (url is ApplicationUrl)
                return ((ApplicationUrl)url).IsSecure;
            else if (url is ReadOnlyApplicationUrl)
                return ((ReadOnlyApplicationUrl)url).IsSecure;
            else
                return null;
        }

        public static ApplicationUrlState Copy(ReadOnlyUrl url)
        {
            if (url is ApplicationUrl)
                return ((ApplicationUrl)url).State.Clone();
            else if (url is ReadOnlyApplicationUrl)
                return ((ReadOnlyApplicationUrl)url).State.Clone();
            else
                return new ApplicationUrlState();
        }
    }
}