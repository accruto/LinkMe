
use [LinkMeTags]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbl_Association]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbl_Association]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbl_Count]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbl_Count]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbl_Tag]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbl_Tag]
GO

CREATE TABLE [dbo].[tbl_Association] (
	[id] [bigint] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,
	[InstanceId] [bigint] NOT NULL ,
	[TagId] [bigint] NOT NULL ,
	[Class] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbl_Count] (
	[id] [bigint] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,
	[TagId] [bigint] NOT NULL ,
	[Count] [int] NOT NULL ,
	[Class] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbl_Tag] (
	[id] [bigint] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,
	[Tag] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbl_Association] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_Association] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tbl_Count] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_Count] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tbl_Tag] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_Tag] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tbl_Association] WITH NOCHECK ADD 
	CONSTRAINT [IX_tbl_Association] UNIQUE  NONCLUSTERED 
	(
		[InstanceId],
		[TagId],
		[Class]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tbl_Count] WITH NOCHECK ADD 
	CONSTRAINT [DF_tbl_Count_Count] DEFAULT (0) FOR [Count]
GO

