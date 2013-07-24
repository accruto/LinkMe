using System.Web;

namespace LinkMe.Framework.Utility.Urls
{
    public class ApplicationUrl
        : Url
    {
        private ApplicationUrlState _state = new ApplicationUrlState();

        internal ApplicationUrl()
        {
        }

        #region Request

        public ApplicationUrl(HttpRequest request)
            : base(request.Url)
        {
            _state = new ApplicationUrlState();
        }

        #endregion

        #region Application

        private ApplicationUrl(Application application, bool? secure, string url, ReadOnlyQueryString queryString)
            : base(application.GetAbsoluteUrl(url), queryString)
        {
            _state = new ApplicationUrlState(application, url);
            SetSecure(secure);
        }

        #endregion

        #region Application.Current

        public ApplicationUrl(bool? secure, string url, ReadOnlyQueryString queryString)
            : this(Application.Current, secure, url, queryString)
        {
        }

        public ApplicationUrl(string url, ReadOnlyQueryString queryString)
            : this(Application.Current, null, url, queryString)
        {
        }

        public ApplicationUrl(bool? secure, string url)
            : this(Application.Current, secure, url, null)
        {
        }

        public ApplicationUrl(string url)
            : this((bool?)null, url)
        {
        }

        public ApplicationUrl(ReadOnlyUrl url, string relativeUrl)
            : base(url, relativeUrl)
        {
            _state = new ApplicationUrlState(url);
        }

        #endregion

        public override string ToString()
        {
            return _state.ToString(this) ?? base.ToString();
        }

        public string AppRelativePath
        {
            get { return _state.GetAppRelativePath(Path); }
        }

        public string AppRelativePathAndQuery
        {
            get { return _state.GetAppRelativePath(PathAndQuery); }
        }

        public bool IsAbsolute
        {
            get { return _state.IsAbsolute; }
        }

        public bool? IsSecure
        {
            get { return _state.IsSecure; }
        }

        protected override ReadOnlyUrl Create()
        {
            return new ApplicationUrl();
        }

        protected override Url CreateNonReadOnly()
        {
            return new ApplicationUrl();
        }

        internal ApplicationUrlState State
        {
            get { return _state; }
        }

        protected override void Copy(ReadOnlyUrl url, ReadOnlyQueryString queryString)
        {
            base.Copy(url, queryString);
            _state = ApplicationUrlState.Copy(url);
        }

        protected override string GetHost()
        {
            return _state.IsAbsolute ? base.GetHost() : _state.Application.Host;
        }

        protected override void SetHost(string host)
        {
            base.SetHost(host);

            // If the host is explicitly set then the url is absolute.

            _state.IsAbsolute = true;
        }

        protected override string GetScheme()
        {
            // If the url is absolute, or a scheme has been specified then just return it,
            // else return a contextual scheme.

            return _state.IsAbsolute || _state.IsSecure != null ? base.GetScheme() : _state.Application.Scheme;
        }

        protected override void SetScheme(string value)
        {
            base.SetScheme(value);

            switch (value)
            {
                case SecureScheme:
                    _state.IsSecure = true;
                    break;

                case InsecureScheme:
                    _state.IsSecure = false;
                    break;
            }
        }

        private void SetSecure(bool? secure)
        {
            if (secure.HasValue)
            {
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
    }
}