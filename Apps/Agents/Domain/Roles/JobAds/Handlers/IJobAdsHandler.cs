using System;

namespace LinkMe.Apps.Agents.Domain.Roles.JobAds.Handlers
{
    public interface IJobAdsHandler
    {
        void OnApplicationSubmitted(Guid applicationId);
    }
}
