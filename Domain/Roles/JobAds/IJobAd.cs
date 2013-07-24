using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds
{
    public interface IJobAdIntegration
    {
        Guid? IntegratorUserId { get; }
        string ExternalApplyUrl { get; }
        string ExternalApplyApiUrl { get; }
    }

    public interface IJobAdApplication
    {
        bool IncludeCoverLetter { get; }
        IList<IApplicationQuestion> Questions { get; }
    }

    public interface IJobAd
    {
        Guid Id { get; }
        Guid PosterId { get; }
        IJobAdIntegration Integration { get; }
        IJobAdApplication Application { get; }
    }
}
