using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Candidates
{
    public interface ICandidatesRepository
    {
        void CreateCandidate(Candidate candidate);
        void UpdateCandidate(Candidate candidate);
        Candidate GetCandidate(Guid id);
        IList<Candidate> GetCandidates(IEnumerable<Guid> ids);

        void AddResume(Guid candidateId, Guid resumeId, Guid? parsedFromFileId);
        void UpdateResume(Guid candidateId, Guid resumeId, Guid? parsedFromFileId);

        void AddStatusWorkflow(Guid candidateId, Guid workflowId);
        void DeleteStatusWorkflow(Guid candidateId);
        Guid? GetStatusWorkflowId(Guid candidateId);
        IList<Tuple<Guid, CandidateStatus>> GetCandidatesWithoutStatusWorkflow();

        void AddSuggestedJobsWorkflow(Guid candidateId, Guid workflowId);
        void DeleteSuggestedJobsWorkflow(Guid candidateId);
        Guid? GetSuggestedJobsWorkflowId(Guid candidateId);
        IList<Tuple<Guid, CandidateStatus>> GetCandidatesWithoutSuggestedJobsWorkflow();

        void AddActivationEmailWorkflow(Guid candidateId, Guid workflowId);
        void DeleteActivationEmailWorkflow(Guid candidateId);
        Guid? GetActivationEmailWorkflowId(Guid candidateId);

        void CreateDiary(Guid candidateId, Diary diary);
        Diary GetDiary(Guid id);
        Diary GetDiaryByCandidateId(Guid candidateId);

        void CreateDiaryEntry(Guid diaryId, DiaryEntry entry);
        void UpdateDiaryEntry(DiaryEntry entry);
        void DeleteDiaryEntry(Guid entryId);
        DiaryEntry GetDiaryEntry(Guid entryId);
        IList<DiaryEntry> GetDiaryEntries(Guid diaryId);

        void CreateResumeFile(Guid candidateId, ResumeFileReference resumeFileReference);
        void UpdateResumeFile(ResumeFileReference resumeFileReference);
        ResumeFileReference GetResumeFile(Guid candidateId, Guid fileReferenceId);
        ResumeFileReference GetResumeFile(Guid resumeId);
        bool HasResumeFiles(Guid candidateId);
        IList<ResumeFileReference> GetResumeFiles(Guid candidateId);
        ResumeFileReference GetLastUsedResumeFile(Guid candidateId);
    }
}
