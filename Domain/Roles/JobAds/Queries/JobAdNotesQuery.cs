using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds.Queries
{
    public class JobAdNotesQuery
        : IJobAdNotesQuery
    {
        private readonly IJobAdNotesRepository _repository;

        public JobAdNotesQuery(IJobAdNotesRepository repository)
        {
            _repository = repository;
        }

        T IJobAdNotesQuery.GetNote<T>(Guid id)
        {
            return _repository.GetNote<T>(id);
        }

        IList<T> IJobAdNotesQuery.GetNotes<T>(Guid ownerId, Guid jobAdId)
        {
            return _repository.GetNotes<T>(ownerId, jobAdId);
        }

        bool IJobAdNotesQuery.HasNotes(Guid ownerId, Guid jobAdId)
        {
            return _repository.HasNotes(ownerId, jobAdId);
        }

        IList<Guid> IJobAdNotesQuery.GetHasNotesJobAdIds(Guid ownerId)
        {
            return _repository.GetHasNotesJobAdIds(ownerId);
        }
    }
}
