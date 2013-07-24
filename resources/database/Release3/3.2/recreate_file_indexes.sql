CREATE UNIQUE INDEX UQ_FileReference_attributes
ON dbo.FileReference([name], mimeType, dataId)
GO

CREATE UNIQUE INDEX UQ_CandidateResumeFile_candidate_file
ON CandidateResumeFile (candidateId, fileId)
GO
