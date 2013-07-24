using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Images
{
    public class InvalidImageExtensionException
        : UserException
    {
        private readonly string _extension;

        public InvalidImageExtensionException(string extension)
        {
            _extension = extension;
        }

        public string Extension
        {
            get { return _extension; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { _extension };
        }

        public override string Message
        {
            get { return "The extension '" + _extension + "' is not supported."; }
        }
    }
}
