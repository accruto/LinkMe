using System;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Users.Employers.Views
{
    [Serializable]
    public class TooManyAccessesException
        : UserException
    {
    }
}
