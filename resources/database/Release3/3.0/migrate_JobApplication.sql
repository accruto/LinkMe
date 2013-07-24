EXEC sp_changeobjectowner 'linkme_owner.JobApplication', dbo
GO

-- Change applicantId and resumeId to UNIQUEIDENTIFIER, updating constraints.

ALTER TABLE [dbo].[JobApplication] DROP CONSTRAINT [FK_JobApplication_networker_profile]

ALTER TABLE [dbo].[JobApplication] DROP CONSTRAINT [FK_JobApplication_networker_resume_data]

GO


EXEC sp_rename 'dbo.JobApplication.applicantId', '_applicantId', 'COLUMN'

EXEC sp_rename 'dbo.JobApplication.resumeId', '_resumeId', 'COLUMN'

ALTER TABLE dbo.JobApplication
ADD applicantId UNIQUEIDENTIFIER NULL

ALTER TABLE dbo.JobApplication
ADD resumeId UNIQUEIDENTIFIER NULL

GO

UPDATE dbo.JobApplication
SET applicantId = dbo.GuidFromString(_applicantId), resumeId = dbo.GuidFromString(_resumeId)

GO

ALTER TABLE dbo.JobApplication
ALTER COLUMN applicantId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE dbo.JobApplication
DROP COLUMN _applicantId

ALTER TABLE dbo.JobApplication
DROP COLUMN _resumeId

GO

ALTER TABLE dbo.JobApplication
ADD CONSTRAINT FK_JobApplication_ApplicantCandidate 
FOREIGN KEY (applicantId) REFERENCES dbo.Candidate ([id])

ALTER TABLE dbo.JobApplication
ADD CONSTRAINT FK_JobApplication_Resume
FOREIGN KEY (resumeId) REFERENCES dbo.Resume ([id])

GO

-- Change the ResumeAttachment foreign key to point to FileReference

ALTER TABLE dbo.JobApplication
DROP CONSTRAINT FK_JobApplication_File

ALTER TABLE dbo.JobApplication
ADD CONSTRAINT FK_JobApplication_ResumeAttachmentFile
FOREIGN KEY (resumeAttachmentFileId) REFERENCES FileReference ([id])

GO
