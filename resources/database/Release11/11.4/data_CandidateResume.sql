DELETE dbo.CandidateResume
GO

INSERT
	dbo.CandidateResume (candidateId, resumeId, parsedFromFileId)
SELECT
	candidateId, id, parsedFromFileId
FROM
	dbo.Resume

