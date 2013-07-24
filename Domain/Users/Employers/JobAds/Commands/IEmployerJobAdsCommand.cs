using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Employers.JobAds.Commands
{
    public interface IEmployerJobAdsCommand
    {
        void CreateJobAd(IEmployer employer, JobAd jobAd);
        void UpdateJobAd(IEmployer employer, JobAd jobAd);
        void OpenJobAd(IEmployer employer, JobAd jobAd, bool checkLimits);
        void CloseJobAd(IEmployer employer, JobAdEntry jobAd);
        void DeleteJobAd(IEmployer employer, JobAdEntry jobAd);
        void TransferJobAd(IEmployer employer, JobAdEntry jobAd);
    }
}