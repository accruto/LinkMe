using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Candidates.Commands
{
    public class CandidateDiariesCommand
        : ICandidateDiariesCommand
    {
        private readonly ICandidatesRepository _repository;

        public CandidateDiariesCommand(ICandidatesRepository repository)
        {
            _repository = repository;
        }

        void ICandidateDiariesCommand.CreateDiary(Guid candidateId, Diary diary)
        {
            diary.Prepare();
            diary.Validate();
            _repository.CreateDiary(candidateId, diary);
        }

        void ICandidateDiariesCommand.CreateDiaryEntry(Guid diaryId, DiaryEntry entry)
        {
            entry.Prepare();
            entry.Validate();
            _repository.CreateDiaryEntry(diaryId, entry);
        }

        void ICandidateDiariesCommand.UpdateDiaryEntry(DiaryEntry entry)
        {
            entry.Validate();
            _repository.UpdateDiaryEntry(entry);
        }

        void ICandidateDiariesCommand.DeleteDiaryEntry(Guid entryId)
        {
            _repository.DeleteDiaryEntry(entryId);
        }
    }
}