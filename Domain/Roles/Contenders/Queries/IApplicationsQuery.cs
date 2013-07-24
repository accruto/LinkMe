using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Contenders.Queries
{
    public interface IApplicationsQuery
    {
        TApplication GetApplication<TApplication>(Guid id, bool includePending) where TApplication : Application;
        TApplication GetApplication<TApplication>(Guid applicantId, Guid positionId, bool includePending) where TApplication : Application;

        IList<TApplication> GetApplications<TApplication>(Guid applicantId, bool includePending) where TApplication : Application;
        IList<TApplication> GetApplications<TApplication>(IEnumerable<Guid> ids, bool includePending) where TApplication : Application;
        IList<TApplication> GetApplications<TApplication>(Guid applicantId, IEnumerable<Guid> positionIds, bool includePending) where TApplication : Application;
        IList<TApplication> GetApplicationsByPositionId<TApplication>(Guid positionId, bool includePending) where TApplication : Application;
    }
}