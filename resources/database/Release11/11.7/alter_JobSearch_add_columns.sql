ALTER TABLE [dbo].[JobSearch]
	ADD [searcherIp] [varchar](39) NULL DEFAULT (NULL)
GO

ALTER TABLE [dbo].[JobSearch]
	ADD [savedSearchId] [uniqueidentifier] NULL
GO

ALTER TABLE [dbo].[JobSearch]
	ADD [channelId] [uniqueidentifier] NULL
GO

ALTER TABLE [dbo].[JobSearch]
	ADD [appId] [uniqueidentifier] NULL
GO
