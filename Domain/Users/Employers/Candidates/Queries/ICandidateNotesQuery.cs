using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates.Queries
{
    public interface ICandidateNotesQuery
    {
        CandidateNote GetNote(IEmployer employer, Guid id);
        IList<CandidateNote> GetNotes(IEmployer employer, Guid candidateId);

        int GetNoteCount(IEmployer employer, Guid candidateId);
        IDictionary<Guid, int> GetNoteCounts(IEmployer employer, IEnumerable<Guid> candidateIds);

        bool HasNotes(IEmployer employer, Guid candidateId);
        IList<Guid> GetHasNotesCandidateIds(IEmployer employer);
    }
}
