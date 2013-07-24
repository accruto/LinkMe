using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Users.Employers.JobAds.Queries
{
    public interface IJobAdApplicationSubmissionsQuery
    {
        bool HasSubmittedApplication(Guid applicantId, Guid jobAdId);
        IList<Guid> GetSubmittedApplicationJobAdIds(Guid applicantId);
        IList<Guid> GetSubmittedApplicationJobAdIds(Guid applicantId, IEnumerable<Guid> jobAdIds);
    }
}