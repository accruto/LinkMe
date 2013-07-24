using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility.Validation
{
    public class DefaultErrorHandler
        : IErrorHandler
    {
        string IErrorHandler.FormatErrorMessage(UserException exception)
        {
            return exception.Message;
        }

        string IErrorHandler.FormatErrorMessage(ValidationError error)
        {
            return error.Message;
        }

        string IErrorHandler.GetErrorCode(UserException exception)
        {
            return ErrorCodes.GetErrorCode(exception);
        }

        string IErrorHandler.GetErrorCode(ValidationError error)
        {
            return ErrorCodes.GetErrorCode(error);
        }
    }
}
