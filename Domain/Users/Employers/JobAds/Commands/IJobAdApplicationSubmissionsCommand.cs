using System;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Employers.JobAds.Commands
{
    public interface IJobAdApplicationSubmissionsCommand
    {
        void CreateApplication(IJobAd jobAd, InternalApplication application);
        void SubmitApplication(IJobAd jobAd, InternalApplication application);
        void RevokeApplication(IJobAd jobAd, Guid applicantId);
    }
}