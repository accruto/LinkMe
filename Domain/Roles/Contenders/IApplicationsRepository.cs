using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Contenders
{
    public interface IApplicationsRepository
    {
        void CreateApplication<TApplication>(TApplication application) where TApplication : Application;
        void UpdateApplication<TApplication>(TApplication application) where TApplication : Application;
        void DeleteApplication<TApplication>(Guid id) where TApplication : Application;

        TApplication GetApplication<TApplication>(Guid id, bool includePending) where TApplication : Application;
        TApplication GetApplication<TApplication>(Guid applicantId, Guid positionId, bool includePending) where TApplication : Application;
        IList<TApplication> GetApplications<TApplication>(Guid applicantId, bool includePending) where TApplication : Application;
        IList<TApplication> GetApplicationsByPositionId<TApplication>(Guid positionId, bool includePending) where TApplication : Application;
        IList<TApplication> GetApplications<TApplication>(IEnumerable<Guid> ids, bool includePending) where TApplication : Application;
        IList<TApplication> GetApplications<TApplication>(Guid applicantId, IEnumerable<Guid> positionIds, bool includePending) where TApplication : Application;
    }
}