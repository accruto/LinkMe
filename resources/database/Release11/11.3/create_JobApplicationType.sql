BEGIN TRANSACTION
GO
CREATE TYPE [dbo].[JobApplicationType] FROM [tinyint] NOT NULL
GO
ALTER TABLE dbo.JobApplication ADD
	applicationType dbo.JobApplicationType NOT NULL CONSTRAINT DF_JobApplication_applicationType DEFAULT 0
GO
COMMIT
