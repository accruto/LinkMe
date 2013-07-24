using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility.Validation
{
    public enum ErrorCodeClass
    {
        Authorization = 1,
        Validation = 3,
        NotFound = 4,
        ServerError = 5,
    }
    
    public static class ErrorCodes
    {
        private const string UnauthorizedErrorCode = "100";
        private const string ValidationErrorCode = "300";
        private const string NotFoundCode = "400";
        private const string ServerErrorCode = "500";

        public static string GetErrorCode(UserException exception)
        {
            return ValidationErrorCode;
        }

        public static string GetErrorCode(ValidationError exception)
        {
            return ValidationErrorCode;
        }

        public static string GetDefaultErrorCode(ErrorCodeClass errorCodeClass)
        {
            switch (errorCodeClass)
            {
                case ErrorCodeClass.Authorization:
                    return UnauthorizedErrorCode;

                case ErrorCodeClass.NotFound:
                    return NotFoundCode;

                case ErrorCodeClass.ServerError:
                    return ServerErrorCode;

                default:
                    return ValidationErrorCode;
            }
        }

        public static ErrorCodeClass GetErrorCodeClass(string errorCode)
        {
            if (errorCode.StartsWith("1"))
                return ErrorCodeClass.Authorization;
            if (errorCode.StartsWith("4"))
                return ErrorCodeClass.NotFound;
            if (errorCode.StartsWith("5"))
                return ErrorCodeClass.ServerError;
            return ErrorCodeClass.Validation;
        }
    }
}
