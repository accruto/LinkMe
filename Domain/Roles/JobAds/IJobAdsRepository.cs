using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Roles.JobAds
{
    public interface IJobAdsRepository
    {
        void CreateJobAd(JobAd jobAd);
        void UpdateJobAd(JobAd jobAd);
        void UpdateJobAd(JobAdEntry jobAd);
        void TransferJobAd(Guid toPosterId, Guid jobAdId);

        T GetJobAd<T>(Guid id) where T : JobAdEntry;
        IList<T> GetJobAds<T>(IEnumerable<Guid> ids) where T : JobAdEntry;

        IList<Guid> GetJobAdIds(Guid posterId);
        IList<Guid> GetJobAdIds(Guid posterId, JobAdStatus status);

        IList<Guid> GetJobAdIds(Guid integratorUserId, Guid posterId);
        IList<Guid> GetJobAdIds(Guid integratorUserId, Guid posterId, string externalReferenceId);
        IList<Guid> GetJobAdIds(string externalReferenceId);
        IList<Guid> GetJobAdIds(Guid integratorUserId, string integratorReferenceId);

        IList<Guid> GetJobAdIds();
        IList<Guid> GetOpenJobAdIds();
        IList<Guid> GetOpenJobAdIds(Guid posterId);
        IList<Guid> GetOpenJobAdIds(DateTimeRange createdTimeRange);
        IList<Guid> GetOpenJobAdIds(string referenceId, string title);
        IList<Guid> GetOpenJobAdIds(Guid integratorUserId, Guid posterId);
        IList<Guid> GetOpenJobAdIds(Guid integratorUserId, string integratorReferenceId);
        IList<Guid> GetOpenJobAdIds(IEnumerable<Guid> excludedIntegratorUserIds);
        IList<Guid> GetOpenJobAdIds(IEnumerable<Guid> industryIds, DateTime? modifiedSince);
        IList<Guid> GetRecentOpenJobAdIds(Range range);

        IList<Guid> GetJobAdIdsWithoutSalaries(bool onlyOpenJobAds);
        IList<Guid> GetJobAdIdsWithoutSeniorityIndex();
        IList<Guid> GetExpiredJobAdIds();
        IList<Guid> GetJobAdIdsRequiringRefresh(DateTime lastRefreshTime);
        Tuple<int, int> GetOpenJobAdCounts(Guid posterId);

        void CreateRefresh(Guid jobAdId, DateTime time);
        void UpdateRefresh(Guid jobAdId, DateTime time);
        void DeleteRefresh(Guid jobAdId);
        DateTime? GetLastRefreshTime(Guid jobAdId);

        void ChangeStatus(Guid jobAdId, JobAdStatus newStatus, DateTime? newExpiryTime, DateTime time);
        IList<JobAdStatusChange> GetStatusChanges(Guid jobAdId);
        void UpdateStatusChange(JobAdStatusChange change);

        Guid? GetLastUsedLogoId(Guid posterId);

        void CreateApplicationRequirements(Guid jobAdId, string requirementsXml);

        void CreateJobPoster(JobPoster poster);
        void UpdateJobPoster(JobPoster poster);
        JobPoster GetJobPoster(Guid id);

        long? GetJobSearchId(Guid jobAdId);
        void CreateJobSearchId(Guid jobAdId, long vacancyId);
        void DeleteJobSearchId(Guid jobAdId);
    }
}
