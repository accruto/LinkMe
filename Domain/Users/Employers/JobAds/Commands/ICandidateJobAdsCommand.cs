using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.JobAds;

namespace LinkMe.Domain.Users.Employers.Candidates.Commands
{
    public interface ICandidateJobAdsCommand
    {
        void CreatePrivateCandidateFolder(IEmployer employer, CandidateJobAd jobAd);
        void CreateSharedCandidateFolder(IEmployer employer, CandidateJobAd jobAd);

        bool CanDeleteCandidateFolder(IEmployer employer, CandidateJobAd jobAd);
        bool CanRenameCandidateFolder(IEmployer employer, CandidateJobAd jobAd);

        void DeleteCandidateFolder(IEmployer employer, Guid jobAdId);
        void UndeleteCandidateFolder(IEmployer employer, CandidateJobAd jobAd);
        void RenameCandidateFolder(IEmployer employer, CandidateJobAd jobAd, string name);
    }
}