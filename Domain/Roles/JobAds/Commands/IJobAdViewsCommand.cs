using System;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public interface IJobAdViewsCommand
    {
        void ViewJobAd(Guid? viewerId, Guid jobAdId);
    }
}
