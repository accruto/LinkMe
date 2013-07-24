-- IX_CandidateNote_Searcher_Candidate was a unique index - multiple notes are
-- now allowed, so re-create it without the UNIQUE constraint.

if exists (select * from dbo.sysindexes where name = N'IX_CandidateNote_Searcher_Candidate' and id = object_id(N'[dbo].[CandidateNote]'))
drop index [dbo].[CandidateNote].[IX_CandidateNote_Searcher_Candidate]
GO

CREATE INDEX IX_CandidateNote_Searcher_Candidate
ON dbo.CandidateNote (searcherId, candidateId)
GO
