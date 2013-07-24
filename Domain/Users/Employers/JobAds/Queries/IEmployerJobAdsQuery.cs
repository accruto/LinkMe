using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Employers.JobAds.Queries
{
    public interface IEmployerJobAdsQuery
    {
        T GetJobAd<T>(IEmployer employer, Guid jobAdId) where T : JobAdEntry;
    }
}
