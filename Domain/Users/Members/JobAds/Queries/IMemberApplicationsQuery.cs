using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Contenders;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public interface IMemberApplicationsQuery
    {
        InternalApplication GetInternalApplication(Guid id);
        InternalApplication GetInternalApplication(Guid applicantId, Guid positionId);

        ExternalApplication GetExternalApplication(Guid id);
        ExternalApplication GetExternalApplication(Guid applicantId, Guid positionId);

        IList<Application> GetApplications(Guid applicantId);
        IList<Application> GetApplications(IEnumerable<Guid> ids);
    }
}
