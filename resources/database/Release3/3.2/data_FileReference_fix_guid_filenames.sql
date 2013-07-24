-- Drop indexes that stop this from working.

if exists (select * from dbo.sysindexes where name = N'UQ_CandidateResumeFile_candidate_file' and id = object_id(N'[dbo].[CandidateResumeFile]'))
drop index [dbo].[CandidateResumeFile].[UQ_CandidateResumeFile_candidate_file]
GO

if exists (select * from dbo.sysindexes where name = N'UQ_FileReference_attributes' and id = object_id(N'[dbo].[FileReference]'))
drop index [dbo].[FileReference].[UQ_FileReference_attributes]
GO

-- Update file names (creating some duplicates).

UPDATE FileReference
SET [name] = ru.firstName + ' ' + ru.lastName + fd.extension
FROM dbo.CandidateResumeFile AS crf
INNER JOIN FileReference fr
ON crf.fileId = fr.id
INNER JOIN FileData fd
ON fr.dataId = fd.[id]
INNER JOIN RegisteredUser ru
ON crf.candidateId = ru.[id]
WHERE fr.[name] LIKE '________________________________[_]________[_]______.%'

GO
