using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders.Queries;

namespace LinkMe.Domain.Users.Employers.Candidates.Queries
{
    public class CandidateNotesQuery
        : ICandidateNotesQuery
    {
        private readonly IContenderNotesQuery _contenderNotesQuery;

        public CandidateNotesQuery(IContenderNotesQuery contenderNotesQuery)
        {
            _contenderNotesQuery = contenderNotesQuery;
        }

        CandidateNote ICandidateNotesQuery.GetNote(IEmployer employer, Guid id)
        {
            if (employer == null)
                return null;
            var note = _contenderNotesQuery.GetNote<CandidateNote>(id);
            return CanAccessNote(employer, note) ? note : null;
        }

        IList<CandidateNote> ICandidateNotesQuery.GetNotes(IEmployer employer, Guid candidateId)
        {
            return employer == null
                ? new List<CandidateNote>()
                : _contenderNotesQuery.GetNotes<CandidateNote>(employer.Id, employer.Organisation.Id, candidateId);
        }

        int ICandidateNotesQuery.GetNoteCount(IEmployer employer, Guid candidateId)
        {
            return employer == null
                ? 0
                : _contenderNotesQuery.GetNoteCount(employer.Id, employer.Organisation.Id, candidateId);
        }

        IDictionary<Guid, int> ICandidateNotesQuery.GetNoteCounts(IEmployer employer, IEnumerable<Guid> candidateIds)
        {
            return employer == null
                ? new Dictionary<Guid, int>() 
                : _contenderNotesQuery.GetNoteCounts(employer.Id, employer.Organisation.Id, candidateIds);
        }

        bool ICandidateNotesQuery.HasNotes(IEmployer employer, Guid candidateId)
        {
            return employer != null
                && _contenderNotesQuery.HasNotes(employer.Id, employer.Organisation.Id, candidateId);
        }

        IList<Guid> ICandidateNotesQuery.GetHasNotesCandidateIds(IEmployer employer)
        {
            return employer == null
                ? new List<Guid>()
                : _contenderNotesQuery.GetHasNotesContenderIds(employer.Id, employer.Organisation.Id);
        }

        private static bool CanAccessNote(IEmployer employer, CandidateNote note)
        {
            if (employer == null || note == null)
                return false;

            if (!note.IsShared)
            {
                // Must be the owner.

                return note.RecruiterId == employer.Id;
            }

            // Must belong to the same organisation.

            return note.OrganisationId == employer.Organisation.Id;
        }
    }
}