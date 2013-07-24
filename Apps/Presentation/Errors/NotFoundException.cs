using System;
using System.Web;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Presentation.Errors
{
    public class NotFoundException
        : UserException
    {
        private readonly string _type;

        public NotFoundException(string type)
        {
            _type = type;
        }

        public string Type
        {
            get { return _type; }
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Type) };
        }

        public override string Message
        {
            get { return string.Format("The {0} cannot be found.", _type); }
        }
    }

    public static class NotFoundExtensions
    {
        public static bool IsNotFoundError(this Exception exception)
        {
            // If a page references a control which cannot be found an HttpParseException with code 404 is thrown,
            // but we want the normal Server Error page in this case, so only return true if the exact type
            // is HttpException.

            var httpEx = exception as HttpException;
            return httpEx != null && httpEx.GetHttpCode() == 404 && httpEx.GetType() == typeof(HttpException);
        }
    }
}
