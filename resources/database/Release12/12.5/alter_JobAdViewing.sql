IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdViewing_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdViewing]'))
ALTER TABLE [dbo].[JobAdViewing] DROP CONSTRAINT [FK_JobAdViewing_JobAd]
GO