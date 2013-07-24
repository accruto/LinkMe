IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ProductGrant') AND type in (N'U'))
DROP TABLE dbo.ProductGrant
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ProductGrantReason') AND type in (N'U'))
DROP TABLE dbo.ProductGrantReason
GO

EXEC sp_fulltext_table 'dbo.JobAd', 'Drop'
EXEC sp_fulltext_catalog 'JobAdCatalog', 'Drop'
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdLocation_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdLocation]'))
ALTER TABLE [dbo].[JobAdLocation] DROP CONSTRAINT [FK_JobAdLocation_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdIndustry_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdIndustry]'))
ALTER TABLE [dbo].[JobAdIndustry] DROP CONSTRAINT [FK_JobAdIndustry_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserEventActionedJobAd_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserEventActionedJobAd]'))
ALTER TABLE [dbo].[UserEventActionedJobAd] DROP CONSTRAINT [FK_UserEventActionedJobAd_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdViewing_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdViewing]'))
ALTER TABLE [dbo].[JobAdViewing] DROP CONSTRAINT [FK_JobAdViewing_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdStatus_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdStatus]'))
ALTER TABLE [dbo].[JobAdStatus] DROP CONSTRAINT [FK_JobAdStatus_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobAdExport_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobAdExport]'))
ALTER TABLE [dbo].[JobAdExport] DROP CONSTRAINT [FK_JobAdExport_JobAd]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_JobSearchResult_JobAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[JobSearchResult]'))
ALTER TABLE [dbo].[JobSearchResult] DROP CONSTRAINT [FK_JobSearchResult_JobAd]

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[JobAd]') AND name = N'PK_JobAd')
ALTER TABLE [dbo].[JobAd] DROP CONSTRAINT [PK_JobAd]



IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[JobAd]') AND name = N'IX_JobAd_createdTime')
DROP INDEX [IX_JobAd_createdTime] ON [dbo].[JobAd] WITH ( ONLINE = OFF )

CREATE CLUSTERED INDEX [IX_JobAd_createdTime] ON [dbo].[JobAd] 
(
	[createdTime]
)
GO



ALTER TABLE [dbo].[JobAd] ADD  CONSTRAINT [PK_JobAd] PRIMARY KEY NONCLUSTERED 
(
	[id]
)

ALTER TABLE [dbo].[JobAdViewing]  WITH CHECK ADD  CONSTRAINT [FK_JobAdViewing_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE [dbo].[JobAdStatus]  WITH CHECK ADD  CONSTRAINT [FK_JobAdStatus_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE [dbo].[JobAdExport]  WITH CHECK ADD  CONSTRAINT [FK_JobAdExport_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE [dbo].[JobSearchResult]  WITH CHECK ADD  CONSTRAINT [FK_JobSearchResult_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE [dbo].[UserEventActionedJobAd]  WITH CHECK ADD  CONSTRAINT [FK_UserEventActionedJobAd_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE [dbo].[JobAdIndustry]  WITH CHECK ADD  CONSTRAINT [FK_JobAdIndustry_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])

ALTER TABLE [dbo].[JobAdLocation]  WITH CHECK ADD  CONSTRAINT [FK_JobAdLocation_JobAd] FOREIGN KEY([jobAdId])
REFERENCES [dbo].[JobAd] ([id])
