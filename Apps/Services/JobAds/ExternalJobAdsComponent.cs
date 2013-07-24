using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;

namespace LinkMe.Apps.Services.JobAds
{
    public abstract class ExternalJobAdsComponent
    {
        protected readonly IJobAdsQuery _jobAdsQuery;
        protected readonly IJobAdIntegrationQuery _jobAdIntegrationQuery;

        protected ExternalJobAdsComponent(IJobAdsQuery jobAdsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery)
        {
            _jobAdsQuery = jobAdsQuery;
            _jobAdIntegrationQuery = jobAdIntegrationQuery;
        }

        protected IList<TJobAd> GetOrderedJobAds<TJobAd>(ICollection<Guid> jobAdIds)
            where TJobAd : JobAdEntry
        {
            if (jobAdIds.Count == 0)
                return new List<TJobAd>();

            // Look for job ads that are not deleted.

            var jobAds = _jobAdsQuery.GetJobAds<TJobAd>(jobAdIds);
            if (jobAds.Count == 0)
                return new List<TJobAd>();

            return (from j in jobAds
                    where j.Status != JobAdStatus.Deleted
                    orderby j.CreatedTime descending
                    select j).ToList();
        }
    }
}
