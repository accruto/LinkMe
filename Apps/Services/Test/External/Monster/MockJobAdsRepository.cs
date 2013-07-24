using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Apps.Services.Test.External.Monster
{
    class MockJobAdsRepository
        : IJobAdsRepository
    {
        #region Implementation of IJobAdsRepository

        void IJobAdsRepository.CreateJobAd(JobAd jobAd)
        {
        }

        void IJobAdsRepository.UpdateJobAd(JobAd jobAd)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.UpdateJobAd(JobAdEntry jobAd)
        {
            throw new NotImplementedException();
        }

        T IJobAdsRepository.GetJobAd<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        IList<T> IJobAdsRepository.GetJobAds<T>(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(Guid posterId)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(Guid posterId, JobAdStatus status)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(Guid integratorUserId, Guid posterId)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(Guid integratorUserId, Guid posterId, string externalReferenceId)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(string externalReferenceId)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(Guid integratorUserId, string integratorReferenceId)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds()
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds()
        {
            return new List<Guid>();
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(Guid posterId)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(DateTimeRange createdTimeRange)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(string externalReferenceId, string title)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(Guid integratorUserId, Guid posterId)
        {
            return new List<Guid>();
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(Guid integratorUserId, string integratorReferenceId)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(IEnumerable<Guid> excludedIntegratorUserIds)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(IEnumerable<Guid> industryIds, DateTime? modifiedSince)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetRecentOpenJobAdIds(Range range)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetJobAdIdsWithoutSalaries(bool onlyOpenJobAds)
        {
            return new List<Guid>();
        }

        public IList<Guid> GetJobAdIdsWithoutSeniorityIndex()
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetExpiredJobAdIds()
        {
            throw new NotImplementedException();
        }

        IList<Guid> IJobAdsRepository.GetJobAdIdsRequiringRefresh(DateTime lastRefreshTime)
        {
            throw new NotImplementedException();
        }

        Tuple<int, int> IJobAdsRepository.GetOpenJobAdCounts(Guid posterId)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.CreateRefresh(Guid jobAdId, DateTime time)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.UpdateRefresh(Guid jobAdId, DateTime time)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.DeleteRefresh(Guid jobAdId)
        {
            throw new NotImplementedException();
        }

        DateTime? IJobAdsRepository.GetLastRefreshTime(Guid jobAdId)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.TransferJobAd(Guid toPosterId, Guid jobAdId)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.ChangeStatus(Guid jobAdId, JobAdStatus status, DateTime? expiryTime, DateTime updatedTime)
        { }

        IList<JobAdStatusChange> IJobAdsRepository.GetStatusChanges(Guid jobAdId)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.UpdateStatusChange(JobAdStatusChange change)
        {
            throw new NotImplementedException();
        }

        Guid? IJobAdsRepository.GetLastUsedLogoId(Guid posterId)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.CreateApplicationRequirements(Guid jobAdId, string requirementsXml)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.CreateJobPoster(JobPoster poster)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.UpdateJobPoster(JobPoster poster)
        {
            throw new NotImplementedException();
        }

        JobPoster IJobAdsRepository.GetJobPoster(Guid id)
        {
            throw new NotImplementedException();
        }

        long? IJobAdsRepository.GetJobSearchId(Guid jobAdId)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.CreateJobSearchId(Guid jobAdId, long vacancyId)
        {
            throw new NotImplementedException();
        }

        void IJobAdsRepository.DeleteJobSearchId(Guid jobAdId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}