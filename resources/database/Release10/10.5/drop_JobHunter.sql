ALTER TABLE [dbo].[JobSearch] DROP CONSTRAINT [FK_JobSearch_SearcherJobHunter]
GO

ALTER TABLE [dbo].[SavedJobSearch] DROP CONSTRAINT [FK_SavedJobSearch_OwnerJobHunter]
GO

ALTER TABLE [dbo].[Member] DROP CONSTRAINT [FK_Member_JobHunter]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JobHunter]') AND type in (N'U'))
DROP TABLE [dbo].[JobHunter]
GO
