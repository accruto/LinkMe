using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Candidates.Queries
{
    public interface ICandidateDiariesQuery
    {
        Diary GetDiary(Guid id);
        Diary GetDiaryByCandidateId(Guid candidateId);
        DiaryEntry GetDiaryEntry(Guid entryId);
        IList<DiaryEntry> GetDiaryEntries(Guid diaryId);
    }
}