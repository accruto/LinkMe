using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace LinkMe.Framework.Tools.Performance.Http
{
    /// <summary>
    /// Provides access to the URL components. The Query part is represented as the NameValueCollection of key/value pairs. 
    /// </summary>
    public class Url
    {
        public const string SecureScheme = "https";
        public const int SecurePort = 443;
        public const string InsecureScheme = "http";
        public const int InsecurePort = 80;
        public const char PathSeparatorChar = '/';

        #region Private Fields

        protected const char QueryStringSeparatorChar = '?';

        private static readonly Dictionary<string, int> DefaultPorts;

        private readonly UriBuilder _uriBuilder;
        private readonly NameValueCollection _queryString;

        #endregion

        #region Constructors

        static Url()
        {
            DefaultPorts = new Dictionary<string, int>(7)
            {
                {"http", 80},
                {"https", 443},
                {"ftp", 21},
                {"gopher", 70},
                {"nntp", 119},
                {"mailto", 25},
                {"telnet", 23}
            };
        }

        public Url()
        {
            _uriBuilder = new UriBuilder();
            SuppressDefaultPort(_uriBuilder);
            _queryString = HttpUtility.ParseQueryString(string.Empty); // create empty System.Web.HttpValueCollection
        }

        public Url(Uri uri)
        {
            _uriBuilder = new UriBuilder(uri);
            SuppressDefaultPort(_uriBuilder);
            _queryString = HttpUtility.ParseQueryString(_uriBuilder.Query);
            _uriBuilder.Query = string.Empty;
        }

        public Url(string url)
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

            SuppressDefaultPort(_uriBuilder);
            _queryString = HttpUtility.ParseQueryString(_uriBuilder.Query);
            _uriBuilder.Query = string.Empty;
        }

        public Url(Url url)
        {
            _uriBuilder = new UriBuilder(url._uriBuilder.Uri);
            SuppressDefaultPort(url._uriBuilder);
            SuppressDefaultPort(_uriBuilder);
            _queryString = HttpUtility.ParseQueryString(string.Empty); // create empty System.Web.HttpValueCollection
            _queryString.Add(url._queryString);
        }

        public Url(Url url, string relativeUrl)
            : this(new Uri(new Uri(url.AbsoluteUri), new Uri(relativeUrl, UriKind.Relative)))
        {
        }

        #endregion

        #region Public Properties

        public string AbsoluteUri
        {
            get { return GetAbsoluteUri(); }
        }

        public string Scheme
        {
            get { return _uriBuilder.Scheme; }

            set
            {
                SuppressDefaultPort(_uriBuilder);
                _uriBuilder.Scheme = value;
            }
        }

        public string UserName
        {
            get { return _uriBuilder.UserName; }
            set { _uriBuilder.UserName = value; }
        }

        public string Password
        {
            get { return _uriBuilder.Password; }
            set { _uriBuilder.Password = value; }
        }

        public string Host
        {
            get { return GetHost(); }
            set { SetHost(value); }
        }

        public int Port
        {
            get
            {
                int port = _uriBuilder.Port;
                return (port == -1) ? GetDefaultPort() : port;
            }
            set
            {
                _uriBuilder.Port = value;
                SuppressDefaultPort(_uriBuilder);
            }
        }

        public string Path
        {
            get { return _uriBuilder.Path; }
            set { _uriBuilder.Path = value; }
        }

        public string PathAndQuery
        {
            get
            {
                string queryString = _queryString.ToString();
                string pathAndQuery = queryString == string.Empty ? Path : Path + QueryStringSeparatorChar + _queryString;
                if (!string.IsNullOrEmpty(Fragment))
                    pathAndQuery += Fragment;
                return pathAndQuery;
            }
        }

        public string FileName
        {
            get
            {
                string path = Path;
                int pos = path.LastIndexOf('/');
                return pos == -1 ? path : path.Substring(pos + 1);
            }
        }

        public string Fragment
        {
            get { return _uriBuilder.Fragment; }
            set { _uriBuilder.Fragment = value; }
        }

        public string Extension
        {
            get
            {
                string path = Path;
                if (Path == null)
                    return null;

                int length = path.Length;
                int index = length;
                while (--index >= 0)
                {
                    char ch = path[index];
                    if (ch == '.')
                    {
                        if (index != (length - 1))
                            return path.Substring(index, length - index);
                        else
                            return string.Empty;
                    }

                    if (ch == PathSeparatorChar)
                        break;
                }

                return string.Empty;
            }
        }

        public NameValueCollection QueryString
        {
            get { return _queryString; }
        }

        #endregion

        #region Static methods

        public static bool Equals(Url a, Url b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a == null || b == null)
                return false;

            // A simplistic comparison for now - this may need to be improved.

            return (a.ToString() == b.ToString());
        }

        public static string Combine(string baseUrl, string relativePath)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentException("The base URL must be specified.", "baseUrl");
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentException("The relative path must be specified.", "relativePath");

            if (baseUrl[baseUrl.Length - 1] == PathSeparatorChar)
            {
                // Avoid a double / if the first part ends with / and the second part starts with /.

                if (relativePath[0] == PathSeparatorChar)
                    return baseUrl.Substring(0, baseUrl.Length - 1) + relativePath;
            }
            else if (relativePath[0] != PathSeparatorChar)
            {
                return baseUrl + PathSeparatorChar + relativePath; // Add a / in-between the two URLs when needed.
            }

            return baseUrl + relativePath;
        }

        public static NameValueCollection GetQueryString(params string[] parameters)
        {
            NameValueCollection queryString = new NameValueCollection();
            if (parameters == null)
                return queryString;

            if (parameters.Length % 2 != 0)
                throw new ArgumentException("An even number of arguments must be specified.", "parameters");


#if DEBUG
            Dictionary<string, object> uniqueKeys = new Dictionary<string, object>();
#endif

            for (int index = 0; index < parameters.Length; index += 2)
            {
                // The key must not be null or empty.

                string key = parameters[index];
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentException("The query parameter name at index " + index + " is null or empty string.");

#if DEBUG
                try
                {
                    uniqueKeys.Add(key, null);
                }
                catch (ArgumentException)
                {
                    throw new ApplicationException("The same query string parameter is specified twice: '" + key + "'.");
                }
#endif

                // The value can be null (don't add at all) or empty (add an empty value). This is consistent
                // with what Request.QueryString["key"] would return.

                string value = parameters[index + 1];
                if (value == null)
                    continue;

                queryString[key] = value;
            }

            return queryString;
        }

        public static NameValueCollection GetQueryString(string queryString)
        {
            return string.IsNullOrEmpty(queryString) ? new NameValueCollection() : HttpUtility.ParseQueryString(queryString);
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            return Equals(this, obj as Url);
        }

        public override int GetHashCode()
        {
            return _uriBuilder.GetHashCode() ^ _queryString.Count.GetHashCode();
        }

        public override string ToString()
        {
            return GetAbsoluteUri();
        }

        #endregion

        protected virtual string GetHost()
        {
            return _uriBuilder.Host;
        }

        protected virtual void SetHost(string host)
        {
            _uriBuilder.Host = host;
        }

        private string GetAbsoluteUri()
        {
            // Create a copy because this may be called from multiple threads.

            UriBuilder uriBuilder = new UriBuilder(_uriBuilder.Uri);
            SuppressDefaultPort(uriBuilder);
            uriBuilder.Query = _queryString.ToString();
            uriBuilder.Host = GetHost();
            return uriBuilder.ToString();
        }

        private static void SuppressDefaultPort(UriBuilder uriBuilder)
        {
            int defaultPort;
            if (DefaultPorts.TryGetValue(uriBuilder.Scheme, out defaultPort) && defaultPort == uriBuilder.Port)
                uriBuilder.Port = -1;
        }

        private int GetDefaultPort()
        {
            int defaultPort;
            return DefaultPorts.TryGetValue(_uriBuilder.Scheme, out defaultPort) ? defaultPort : -1;
        }
    }
}
