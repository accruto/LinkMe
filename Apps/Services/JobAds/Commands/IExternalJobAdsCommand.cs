using System;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Services.JobAds.Commands
{
    public interface IExternalJobAdsCommand
    {
        JobAd GetExistingJobAd(Guid integratorUserId, string integratorReferenceId);
        JobAd GetExistingJobAd(Guid integratorUserId, Guid posterId, string externalReferenceId);

        bool CanCreateJobAd(JobAdEntry jobAd);
    }
}
