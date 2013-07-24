using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Contenders.Queries
{
    public class ApplicationsQuery
        : IApplicationsQuery
    {
        private readonly IApplicationsRepository _repository;

        public ApplicationsQuery(IApplicationsRepository repository)
        {
            _repository = repository;
        }

        TApplication IApplicationsQuery.GetApplication<TApplication>(Guid id, bool includePending)
        {
            return _repository.GetApplication<TApplication>(id, includePending);
        }

        TApplication IApplicationsQuery.GetApplication<TApplication>(Guid applicantId, Guid positionId, bool includePending)
        {
            return _repository.GetApplication<TApplication>(applicantId, positionId, includePending);
        }

        IList<TApplication> IApplicationsQuery.GetApplications<TApplication>(Guid applicantId, bool includePending)
        {
            return _repository.GetApplications<TApplication>(applicantId, includePending);
        }

        IList<TApplication> IApplicationsQuery.GetApplications<TApplication>(IEnumerable<Guid> ids, bool includePending)
        {
            return _repository.GetApplications<TApplication>(ids, includePending);
        }

        IList<TApplication> IApplicationsQuery.GetApplications<TApplication>(Guid applicantId, IEnumerable<Guid> positionIds, bool includePending)
        {
            return _repository.GetApplications<TApplication>(applicantId, positionIds, includePending);
        }

        IList<TApplication> IApplicationsQuery.GetApplicationsByPositionId<TApplication>(Guid positionId, bool includePending)
        {
            return _repository.GetApplicationsByPositionId<TApplication>(positionId, includePending);
        }
    }
}