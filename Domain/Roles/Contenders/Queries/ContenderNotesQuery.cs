using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Contenders.Queries
{
    public class ContenderNotesQuery
        : IContenderNotesQuery
    {
        private readonly IContenderNotesRepository _repository;

        public ContenderNotesQuery(IContenderNotesRepository repository)
        {
            _repository = repository;
        }

        T IContenderNotesQuery.GetNote<T>(Guid id)
        {
            return _repository.GetNote<T>(id);
        }

        IList<T> IContenderNotesQuery.GetNotes<T>(Guid ownerId, Guid sharedWithId, Guid contenderId)
        {
            return _repository.GetNotes<T>(ownerId, sharedWithId, contenderId);
        }

        bool IContenderNotesQuery.HasNotes(Guid ownerId, Guid sharedWithId, Guid contenderId)
        {
            return _repository.HasNotes(ownerId, sharedWithId, contenderId);
        }

        IList<Guid> IContenderNotesQuery.GetHasNotesContenderIds(Guid ownerId, Guid sharedWithId)
        {
            return _repository.GetHasNotesContenderIds(ownerId, sharedWithId);
        }

        int IContenderNotesQuery.GetNoteCount(Guid ownerId, Guid sharedWithId, Guid candidateId)
        {
            return _repository.GetNoteCount(ownerId, sharedWithId, candidateId);
        }

        IDictionary<Guid, int> IContenderNotesQuery.GetNoteCounts(Guid ownerId, Guid sharedWithId, IEnumerable<Guid> candidateIds)
        {
            return _repository.GetNoteCounts(ownerId, sharedWithId, candidateIds);
        }
    }
}