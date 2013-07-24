using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Representatives.Queries
{
    public class RepresentativesQuery
        : IRepresentativesQuery
    {
        private readonly IRepresentativesRepository _repository;

        public RepresentativesQuery(IRepresentativesRepository repository)
        {
            _repository = repository;
        }

        Guid? IRepresentativesQuery.GetRepresentativeId(Guid representeeId)
        {
            return _repository.GetRepresentativeId(representeeId);
        }

        IDictionary<Guid, Guid> IRepresentativesQuery.GetRepresentativeIds(IEnumerable<Guid> representeeIds)
        {
            return _repository.GetRepresentativeIds(representeeIds);
        }

        IList<Guid> IRepresentativesQuery.GetRepresenteeIds(Guid representativeId)
        {
            return _repository.GetRepresenteeIds(representativeId);
        }

        IList<Guid> IRepresentativesQuery.GetRepresenteeIds(Guid representativeId, IEnumerable<Guid> representeesId)
        {
            return _repository.GetRepresenteeIds(representativeId, representeesId);
        }
    }
}