-- Drop constarints

ALTER TABLE [dbo].[JobApplication] DROP CONSTRAINT [FK_JobApplication_ApplicantCandidate]
GO

ALTER TABLE [dbo].[JobApplication] DROP CONSTRAINT [FK_JobApplication_JobAd]
GO
