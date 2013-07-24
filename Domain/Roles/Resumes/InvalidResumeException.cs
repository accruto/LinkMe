using System;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Roles.Resumes
{
    public class InvalidResumeException
        : UserException
    {
        public InvalidResumeException()
        {
        }

        public InvalidResumeException(Exception innerException)
            : base(null, innerException)
        {
        }

        public override string Message
        {
            get { return "The resume is invalid."; }
        }
    }

    public class InvalidResumeExtensionException
        : UserException
    {
        private readonly string _extension;

        public InvalidResumeExtensionException(string extension)
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
            get { return "The resume extension '" + _extension + "' is not supported."; }
        }
    }
}