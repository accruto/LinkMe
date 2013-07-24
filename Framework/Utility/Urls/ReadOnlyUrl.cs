using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMe.Framework.Utility.Urls
{
    public class ReadOnlyUrl
    {
        #region Private Fields

        private static readonly Dictionary<string, int> _defaultPorts;

        private UriBuilder _uriBuilder;
        private bool _sharingBuilder;
        private QueryStringCollection _queryString;
        private bool _sharingQueryString;

        #endregion

        #region Constructors

        static ReadOnlyUrl()
        {
            _defaultPorts = new Dictionary<string, int>(7);
            _defaultPorts.Add("http", 80);
            _defaultPorts.Add("https", 443);
            _defaultPorts.Add("ftp", 21);
            _defaultPorts.Add("gopher", 70);
            _defaultPorts.Add("nntp", 119);
            _defaultPorts.Add("mailto", 25);
            _defaultPorts.Add("telnet", 23);
        }

        protected ReadOnlyUrl()
        {
        }

        public ReadOnlyUrl(Uri uri)
        {
            CreateBuilder(uri);
            CreateQueryString(_uriBuilder.Query);
            _uriBuilder.Query = string.Empty;
            SuppressDefaultPort(_uriBuilder);
        }

        public ReadOnlyUrl(string url)
        {
            CreateBuilder(url);
            CreateQueryString(_uriBuilder.Query);
            _uriBuilder.Query = string.Empty;
            SuppressDefaultPort(_uriBuilder);
        }

        public ReadOnlyUrl(string url, ReadOnlyQueryString queryString)
            : this(url)
        {
            SetQueryString(queryString);
        }

        protected ReadOnlyUrl(ReadOnlyUrl url, string relativeUrl, ReadOnlyQueryString queryString)
            : this(new Uri(new Uri(url.AbsoluteUri), new Uri(relativeUrl, UriKind.Relative)))
        {
            SetQueryString(queryString);
        }

        protected ReadOnlyUrl(ReadOnlyUrl url, string relativeUrl)
            : this(url, relativeUrl, null)
        {
        }

        #endregion

        #region Properties

        public string AbsoluteUri
        {
            get
            {
                // Create a copy because derived classes can override aspects.

                var uriBuilder = CreateBuilder(GetScheme(), GetHost(), _uriBuilder.Port, _uriBuilder.Path, _uriBuilder.Fragment);
                if (_queryString.Count > 0)
                    uriBuilder.Query = _queryString.ToString();
                return uriBuilder.ToString();
            }
        }

        public string Scheme
        {
            get { return GetScheme(); }
            protected set { SetScheme(value); }
        }

        public string Host
        {
            get { return GetHost(); }
            protected set { SetHost(value); }
        }

        public int Port
        {
            get
            {
                var port = _uriBuilder.Port;
                return port == -1 ? GetDefaultPort(_uriBuilder) : port;
            }
            protected set
            {
                EnsureOwningBuilder();
                _uriBuilder.Port = value;
                SuppressDefaultPort(_uriBuilder);
            }
        }

        public string Path
        {
            get
            {
                return _uriBuilder.Path;
            }
            protected set
            {
                EnsureOwningBuilder();
                _uriBuilder.Path = value;
            }
        }

        public string AbsolutePath
        {
            get
            {
                // Create a copy because derived classes can override aspects but don't include the query string.

                var uriBuilder = CreateBuilder(GetScheme(), GetHost(), _uriBuilder.Port, _uriBuilder.Path, _uriBuilder.Fragment);
                return uriBuilder.ToString();
            }
        }

        public string Fragment
        {
            get
            {
                return _uriBuilder.Fragment;
            }
            protected set
            {
                EnsureOwningBuilder();
                _uriBuilder.Fragment = value;
            }
        }

        public string PathAndQuery
        {
            get
            {
                string pathAndQuery;
                if (_queryString.Count > 0)
                    pathAndQuery = Path + Url.QueryStringSeparatorChar + _queryString;
                else
                    pathAndQuery = Path;
                if (!string.IsNullOrEmpty(Fragment))
                    pathAndQuery += Fragment;
                return pathAndQuery;
            }
        }

        public string PathWithNoFileName
        {
            get
            {
                var path = Path;
                var pos = path.LastIndexOf(Url.PathSeparatorChar);
                return pos == -1 ? path : path.Substring(0, pos);
            }
        }

        public string FileName
        {
            get
            {
                var path = Path;
                var pos = path.LastIndexOf(Url.PathSeparatorChar);
                return pos == -1 ? path : path.Substring(pos + 1);
            }
        }

        public string Extension
        {
            get
            {
                var path = Path;
                if (path == null)
                    return null;

                var length = path.Length;
                var index = length;
                while (--index >= 0)
                {
                    var ch = path[index];
                    if (ch == '.')
                        return index != (length - 1) ? path.Substring(index, length - index) : string.Empty;

                    if (ch == Url.PathSeparatorChar)
                        break;
                }

                return string.Empty;
            }
        }

        public ReadOnlyQueryString QueryString
        {
            get { return GetReadOnlyQueryString(); }
        }

        #endregion

        #region Public Methods

        public virtual Url AsNonReadOnly()
        {
            // Always create a copy.

            var url = CreateNonReadOnly();
            url.Copy(this, null);
            return url;
        }

        public ReadOnlyUrl RootUrl
        {
            get
            {
                var url = AsNonReadOnly();
                url.Path = string.Empty;
                url.QueryString.Clear();
                return url;
            }
        }

        public string GetRelativeUrl(ReadOnlyUrl relativeToUrl)
        {
            // Only really been tested when both this and relativeToUrl have file names.
            // May need to check if this is not true.

            if (relativeToUrl == null)
                throw new ArgumentNullException("relativeToUrl");

            // If the root urls are different, there is no relative path.

            if (string.Compare(RootUrl.AbsoluteUri, relativeToUrl.RootUrl.AbsoluteUri, true) != 0)
                return AbsoluteUri;

            // Compare paths.

            var segments = PathWithNoFileName.Split(new [] { Url.PathSeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            var relativeToSegments = relativeToUrl.PathWithNoFileName.TrimEnd(Url.PathSeparatorChar).Split(new [] { Url.PathSeparatorChar } , StringSplitOptions.RemoveEmptyEntries);

            var index = 0;
            while (index < segments.Length && index < relativeToSegments.Length && string.Compare(segments[index], relativeToSegments[index], true) == 0)
                index++;

            // Go up the tree to where the paths meet.

            var sb = new StringBuilder();
            if (index < relativeToSegments.Length)
            {
                var upOneLevel = ".." + Url.PathSeparatorChar;
                for (var temp = index; temp < relativeToSegments.Length; temp++)
                    sb.Append(upOneLevel);
            }

            // Go down the tree to 'path'.

            if (index < segments.Length)
            {
                sb.Append(string.Join(Url.PathSeparatorChar.ToString(), segments, index, segments.Length - index));
                sb.Append(Url.PathSeparatorChar);
            }

            sb.Append(FileName);

            return sb.ToString();
        }

        public ReadOnlyUrl Clone()
        {
            return Clone(null);
        }

        public ReadOnlyUrl Clone(ReadOnlyQueryString queryString)
        {
            var url = Create();
            url.Copy(this, queryString);
            return url;
        }

        public static bool Equals(ReadOnlyUrl a, ReadOnlyUrl b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a == null || b == null)
                return false;

            // A simplistic comparison for now - this may need to be improved.

            return (a.ToString() == b.ToString());
        }

        public override bool Equals(object obj)
        {
            return Equals(this, obj as ReadOnlyUrl);
        }

        public override int GetHashCode()
        {
            return _uriBuilder.GetHashCode() ^ _queryString.Count.GetHashCode();
        }

        public override string ToString()
        {
            return AbsoluteUri;
        }

        #endregion

        protected virtual Url CreateNonReadOnly()
        {
            return new Url();
        }

        protected virtual ReadOnlyUrl Create()
        {
            return new ReadOnlyUrl();
        }

        protected virtual void Copy(ReadOnlyUrl url, ReadOnlyQueryString queryString)
        {
            Copy(url);
            SetQueryString(queryString);
        }

        protected virtual string GetHost()
        {
            return _uriBuilder.Host;
        }

        protected virtual void SetHost(string host)
        {
            EnsureOwningBuilder();
            _uriBuilder.Host = host;
        }

        protected virtual string GetScheme()
        {
            return _uriBuilder.Scheme;
        }

        protected virtual void SetScheme(string value)
        {
            EnsureOwningBuilder();
            SuppressDefaultPort(_uriBuilder);
            _uriBuilder.Scheme = value;
        }

        private void Copy(ReadOnlyUrl url)
        {
            _uriBuilder = url._uriBuilder;
            _sharingBuilder = true;
            _queryString = url._queryString;
            _sharingQueryString = true;
        }

        private void EnsureOwningBuilder()
        {
            if (_sharingBuilder)
            {
                _uriBuilder = CreateBuilder(GetScheme(), GetHost(), _uriBuilder.Port, _uriBuilder.Path, _uriBuilder.Fragment);
                _sharingBuilder = true;
            }
        }

        private void EnsureOwningQueryString()
        {
            if (_sharingQueryString)
            {
                CreateQueryString(_queryString);
                _sharingQueryString = false;
            }
        }

        private static int GetDefaultPort(UriBuilder uriBuilder)
        {
            int defaultPort;
            return _defaultPorts.TryGetValue(uriBuilder.Scheme, out defaultPort) ? defaultPort : -1;
        }

        private static void SuppressDefaultPort(UriBuilder uriBuilder)
        {
            int defaultPort;
            if (_defaultPorts.TryGetValue(uriBuilder.Scheme, out defaultPort) && defaultPort == uriBuilder.Port)
                uriBuilder.Port = -1;
        }

        protected ReadOnlyQueryString GetReadOnlyQueryString()
        {
            return new ReadOnlyQueryString(_queryString);
        }

        protected QueryString GetQueryString()
        {
            EnsureOwningQueryString();
            return new QueryString(_queryString);
        }

        private static UriBuilder CreateBuilder(string scheme, string host, int port, string path, string fragment)
        {
            var uriBuilder = new UriBuilder(scheme, host, port, path);
            SuppressDefaultPort(uriBuilder);
            if (!string.IsNullOrEmpty(fragment))
            {
                uriBuilder.Fragment = fragment.TrimStart('#');
            }
            return uriBuilder;
        }

        private void CreateBuilder(Uri uri)
        {
            _uriBuilder = new UriBuilder(uri);
        }

        private void CreateBuilder(string url)
        {
            try
            {
                _uriBuilder = new UriBuilder(url);
            }
            catch (UriFormatException ex)
            {
                // Include the invalid URL in the otherwise-useless error message. The stack trace of the
                // original exception is not much of a loss in this case.
                throw new UriFormatException("Failed to parse URL '" + url + "': " + ex.Message);
            }
        }

        private void CreateQueryString(string queryString)
        {
            _queryString = new QueryStringCollection(queryString);
        }

        private void CreateQueryString(QueryStringCollection queryString)
        {
            _queryString = new QueryStringCollection(queryString);
        }

        private void SetQueryString(ReadOnlyQueryString queryString)
        {
            if (queryString != null && queryString.Count > 0)
            {
                EnsureOwningQueryString();
                _queryString.Set(queryString.Collection);
            }
        }
    }
}
