using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Candidates.Queries
{
    public class CandidateDiariesQuery
        : ICandidateDiariesQuery
    {
        private readonly ICandidatesRepository _repository;

        public CandidateDiariesQuery(ICandidatesRepository repository)
        {
            _repository = repository;
        }

        Diary ICandidateDiariesQuery.GetDiary(Guid id)
        {
            return _repository.GetDiary(id);
        }

        Diary ICandidateDiariesQuery.GetDiaryByCandidateId(Guid candidateId)
        {
            return _repository.GetDiaryByCandidateId(candidateId);
        }

        DiaryEntry ICandidateDiariesQuery.GetDiaryEntry(Guid entryId)
        {
            return _repository.GetDiaryEntry(entryId);
        }

        IList<DiaryEntry> ICandidateDiariesQuery.GetDiaryEntries(Guid diaryId)
        {
            return _repository.GetDiaryEntries(diaryId);
        }
    }
}