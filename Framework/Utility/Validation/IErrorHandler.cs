using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility.Validation
{
    public interface IErrorHandler
    {
        string FormatErrorMessage(UserException exception);
        string FormatErrorMessage(ValidationError error);

        string GetErrorCode(UserException exception);
        string GetErrorCode(ValidationError error);
    }
}
