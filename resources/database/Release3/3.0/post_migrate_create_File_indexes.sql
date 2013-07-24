-- Prevent duplicates in FileData.

CREATE UNIQUE INDEX UQ_FileData_attributes
ON FileData (context, sizeBytes, [hash])
GO

-- ... and in FileReference.

CREATE UNIQUE INDEX UQ_FileReference_attributes
ON FileReference ([name], mimeType, dataId)
GO

-- ... and in CandidateResumeFile

CREATE UNIQUE INDEX UQ_CandidateResumeFile_candidate_file
ON CandidateResumeFile (candidateId, fileId)
GO
