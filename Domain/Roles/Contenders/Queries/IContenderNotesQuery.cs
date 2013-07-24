using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Contenders.Queries
{
    public interface IContenderNotesQuery
    {
        T GetNote<T>(Guid id) where T : ContenderNote, new();
        IList<T> GetNotes<T>(Guid ownerId, Guid sharedWithId, Guid contenderId) where T : ContenderNote, new();

        bool HasNotes(Guid ownerId, Guid sharedWithId, Guid contenderId);
        IList<Guid> GetHasNotesContenderIds(Guid ownerId, Guid sharedWithId);

        int GetNoteCount(Guid ownerId, Guid sharedWithId, Guid candidateId);
        IDictionary<Guid, int> GetNoteCounts(Guid ownerId, Guid sharedWithId, IEnumerable<Guid> candidateIds);
    }
}