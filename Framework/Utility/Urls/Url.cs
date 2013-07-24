using System;

namespace LinkMe.Framework.Utility.Urls
{
    public class Url
        : ReadOnlyUrl
    {
        public const string SecureScheme = "https";
        public const int SecurePort = 443;
        public const string InsecureScheme = "http";
        public const int InsecurePort = 80;
        public const char PathSeparatorChar = '/';
        public const char QueryStringSeparatorChar = '?';

        #region Constructors

        protected internal Url()
        {
        }

        public Url(Uri uri)
            : base(uri)
        {
        }

        public Url(string url, ReadOnlyQueryString queryString)
            : base(url, queryString)
        {
        }

        public Url(string url)
            : base(url)
        {
        }

        public Url(ReadOnlyUrl url, string relativeUrl)
            : base(url, relativeUrl)
        {
        }

        #endregion

        public new Url Clone()
        {
            return (Url)base.Clone();
        }

        #region Properties

        public new string Scheme
        {
            get { return base.Scheme; }
            set { base.Scheme = value; }
        }

        public new string Host
        {
            get { return base.Host; }
            set { base.Host = value; }
        }

        public new int Port
        {
            get { return base.Port; }
            set { base.Port = value; }
        }

        public new string Path
        {
            get { return base.Path; }
            set { base.Path = value; }
        }

        public new string Fragment
        {
            get { return base.Fragment; }
            set { base.Fragment = value; }
        }

        public new QueryString QueryString
        {
            get { return GetQueryString(); }
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            return Equals(this, obj as Url);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        protected override ReadOnlyUrl Create()
        {
            return new Url();
        }
    }
}
