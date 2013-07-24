using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Users
{
    public class DuplicateUserException
        : UserException
    {
        public override string Message
        {
            get { return "The username is already being used."; }
        }
    }
}
