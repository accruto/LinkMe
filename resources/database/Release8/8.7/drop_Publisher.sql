ALTER TABLE [dbo].[JobApplication] DROP CONSTRAINT [FK_JobApplication_Publisher]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[JobAdPublisher]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[JobAdPublisher]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Publisher]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[Publisher]
GO
