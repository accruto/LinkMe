if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Association]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Association]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Count]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Count]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Tag]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Tag]
GO

CREATE TABLE [linkme_owner].[tbl_Association] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[InstanceId] [bigint] NOT NULL ,
	[TagId] [bigint] NOT NULL ,
	[Class] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [linkme_owner].[tbl_Count] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TagId] [bigint] NOT NULL ,
	[Count] [int] NOT NULL ,
	[Class] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [linkme_owner].[tbl_Tag] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[Tag] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [linkme_owner].[tbl_Association] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_Association] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [linkme_owner].[tbl_Count] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_Count] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [linkme_owner].[tbl_Tag] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_Tag] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [linkme_owner].[tbl_Association] WITH NOCHECK ADD 
	CONSTRAINT [IX_tbl_Association] UNIQUE  NONCLUSTERED 
	(
		[InstanceId],
		[TagId],
		[Class]
	)  ON [PRIMARY] 
GO

