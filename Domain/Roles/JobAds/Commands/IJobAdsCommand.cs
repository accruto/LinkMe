using System;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public interface IJobAdsCommand
    {
        T GetJobAd<T>(Guid id) where T : JobAdEntry;

        void CreateJobAd(JobAd jobAd);
        void UpdateJobAd(JobAd jobAd);

        bool CanBeOpened(JobAdEntry jobAd);
        void OpenJobAd(JobAd jobAd);
        void DeleteJobAd(JobAdEntry jobAd);
        bool CanBeClosed(JobAdEntry jobAd);
        void CloseJobAd(JobAdEntry jobAd);
        void TransferJobAd(Guid toPosterId, JobAdEntry jobAd);
        void RefreshJobAd(JobAdEntry jobAd);

        DateTime GetDefaultExpiryTime(JobAdFeatures features);
        Guid? GetLastUsedLogoId(Guid posterId);

        void CreateApplicationRequirements(Guid jobAdId, string requirementsXml);
    }
}