IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdListEntry_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdListEntry]'))
ALTER TABLE [dbo].[JobAdListEntry] DROP CONSTRAINT [FK_JobAdListEntry_JobAd]
GO
