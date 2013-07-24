using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Apps.Presentation.Errors
{
    public class UrlNotFoundException
        : UserException
    {
        private readonly string _url;

        public UrlNotFoundException(string url)
        {
            _url = url;
        }

        public string Url
        {
            get { return _url; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { _url };
        }
    }
}
