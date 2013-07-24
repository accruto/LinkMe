using System;

namespace LinkMe.Domain.Roles.Candidates.Commands
{
    public interface ICandidateDiariesCommand
    {
        void CreateDiary(Guid candidateId, Diary diary);
        void CreateDiaryEntry(Guid diaryId, DiaryEntry entry);
        void UpdateDiaryEntry(DiaryEntry entry);
        void DeleteDiaryEntry(Guid entryId);
    }
}