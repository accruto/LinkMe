using System;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Users.Employers.JobAds
{
    [Serializable]
    public class TooManyJobAdsException
        : UserException
    {
    }
}
