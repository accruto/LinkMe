using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Users.Members.JobAds;

namespace LinkMe.Apps.Services.JobAds.Queries
{
    public interface IExternalJobAdsQuery
    {
        IEmployer GetJobPoster(IntegratorUser integratorUser);
        IEmployer GetJobPoster(IntegratorUser integratorUser, string loginId);
        string GetRedirectName(MemberJobAdView jobAd);

        Guid? GetJobAdId(string externalReferenceId);
    }
}
