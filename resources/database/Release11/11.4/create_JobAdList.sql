IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.JobAdList') AND type in (N'U'))
DROP TABLE dbo.JobAdList
GO

CREATE TABLE [dbo].[JobAdList](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](100) NULL,
	[ownerId] [uniqueidentifier] NOT NULL,
	[createdTime] [datetime] NOT NULL,
	[listType] [int] NOT NULL,
	[isDeleted] [bit] NOT NULL,
)

ALTER TABLE dbo.JobAdList
ADD CONSTRAINT PK_JobAdList PRIMARY KEY NONCLUSTERED
(
	id
)
GO

CREATE CLUSTERED INDEX IX_JobAdList_ownerId ON dbo.JobAdList
(
	ownerId
)
GO

