using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Apps.Presentation.Errors
{
    public class UnauthorizedException
        : UserException
    {
    }

    public class AlreadyAuthorizedException
        : UserException
    {
    }

    public class AuthenticationFailedException
        : UserException
    {
    }

    public class UserDisabledException
        : UserException
    {
    }

    public class UserMustChangePasswordException
        : UserException
    {
    }
}