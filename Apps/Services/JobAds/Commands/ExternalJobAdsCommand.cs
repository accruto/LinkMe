using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Apps.Services.External.JobG8.Queries;
using LinkMe.Apps.Services.External.JXT.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;

namespace LinkMe.Apps.Services.JobAds.Commands
{
    public class ExternalJobAdsCommand
        : ExternalJobAdsComponent, IExternalJobAdsCommand
    {
        private readonly IJobAdsCommand _jobAdsCommand;
        private readonly Guid _jobG8IntegratorUserId;
        private readonly Guid _careerOneIntegratorUserId;
        private readonly Guid _jxtIntegratorUserId;

        public ExternalJobAdsCommand(IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, IJobG8Query jobG8Query, ICareerOneQuery careerOneQuery, IJxtQuery jxtQuery)
            : base(jobAdsQuery, jobAdIntegrationQuery)
        {
            _jobAdsCommand = jobAdsCommand;
            _jobG8IntegratorUserId = jobG8Query.GetIntegratorUser().Id;
            _careerOneIntegratorUserId = careerOneQuery.GetIntegratorUser().Id;
            _jxtIntegratorUserId = jxtQuery.GetIntegratorUser().Id;
        }

        JobAd IExternalJobAdsCommand.GetExistingJobAd(Guid integratorUserId, string integratorReferenceId)
        {
            return GetExistingJobAd(_jobAdIntegrationQuery.GetJobAdIds(integratorUserId, integratorReferenceId));
        }

        JobAd IExternalJobAdsCommand.GetExistingJobAd(Guid integratorUserId, Guid posterId, string externalReferenceId)
        {
            return GetExistingJobAd(_jobAdIntegrationQuery.GetJobAdIds(integratorUserId, posterId, externalReferenceId));
        }

        bool IExternalJobAdsCommand.CanCreateJobAd(JobAdEntry jobAd)
        {
            var equivalentJobAds = GetEquivalentJobAds(jobAd);
            if (equivalentJobAds.Count == 0)
                return true;

            // The job ads must be either Draft or Open.

            equivalentJobAds = (from j in equivalentJobAds where j.Status == JobAdStatus.Draft || j.Status == JobAdStatus.Open select j).ToList();
            if (equivalentJobAds.Count == 0)
                return true;

            // If it has higher priority then any of the equivalent job ads then ...

            if (HasHigherPriority(jobAd, equivalentJobAds))
            {
                // Then close those equivalent job ads so this job ad can replace them.

                foreach (var equivalentJobAd in equivalentJobAds)
                    _jobAdsCommand.CloseJobAd(equivalentJobAd);
                return true;
            }

            return false;
        }

        private JobAd GetExistingJobAd(ICollection<Guid> jobAdIds)
        {
            // Look for existing job ads.

            var jobAds = GetOrderedJobAds<JobAd>(jobAdIds);

            // Return the first job ad and close the others that need it.

            if (jobAds.Count == 0)
                return null;

            foreach (var jobAd in (from j in jobAds.Skip(1) where j.Status == JobAdStatus.Draft || j.Status == JobAdStatus.Open select j))
                _jobAdsCommand.CloseJobAd(jobAd);
            return jobAds[0];
        }

        IList<JobAdEntry> GetEquivalentJobAds(JobAdEntry jobAd)
        {
            // Need something to compare.

            if (string.IsNullOrEmpty(jobAd.Integration.ExternalReferenceId) || string.IsNullOrEmpty(jobAd.Title))
                return new List<JobAdEntry>();

            // Considered equivalent if it has the same ExternalReferenceId and Title.

            var jobAdIds = _jobAdIntegrationQuery.GetOpenJobAdIds(jobAd.Integration.ExternalReferenceId, jobAd.Title);

            // Filter out the job ad in question.

            return _jobAdsQuery.GetJobAds<JobAdEntry>(jobAdIds.Except(new[] { jobAd.Id }));
        }

        bool HasHigherPriority(JobAdEntry jobAd, IEnumerable<JobAdEntry> equivalentJobAds)
        {
            var priority = GetPriority(jobAd);
            return equivalentJobAds.All(j => GetPriority(j) < priority);
        }

        private int GetPriority(JobAdEntry jobAd)
        {
            if (jobAd.Integration.IntegratorUserId == null)
                return 0;

            // JobG8 job ads have the highest priority, then CareerOne, then all others.

            if (jobAd.Integration.IntegratorUserId.Value == _jobG8IntegratorUserId)
                return 3;
            if (jobAd.Integration.IntegratorUserId.Value == _careerOneIntegratorUserId)
                return 2;
            if (jobAd.Integration.IntegratorUserId.Value == _jxtIntegratorUserId)
                return 1;
            return 0;
        }
    }
}
