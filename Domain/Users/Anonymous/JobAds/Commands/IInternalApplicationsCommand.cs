using System;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Anonymous.JobAds.Commands
{
    public interface IInternalApplicationsCommand
    {
        Guid Submit(AnonymousContact contact, IJobAd jobAd, Guid fileReferenceId);
    }
}
