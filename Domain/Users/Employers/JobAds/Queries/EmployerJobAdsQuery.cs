using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Employers.JobAds.Queries
{
    public class EmployerJobAdsQuery
        : IEmployerJobAdsQuery
    {
        private readonly IJobAdsQuery _jobAdsQuery;

        public EmployerJobAdsQuery(IJobAdsQuery jobAdsQuery)
        {
            _jobAdsQuery = jobAdsQuery;
        }

        T IEmployerJobAdsQuery.GetJobAd<T>(IEmployer employer, Guid jobAdId)
        {
            var jobAd = _jobAdsQuery.GetJobAd<T>(jobAdId);
            if (jobAd == null)
                return null;

            return CanAccess(employer, jobAd) ? jobAd : null;
        }

        private static bool CanAccess(IHasId<Guid> employer, IJobAd entry)
        {
            if (employer == null)
                return false;
            return employer.Id == entry.PosterId;
        }
    }
}