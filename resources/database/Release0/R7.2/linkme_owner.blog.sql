if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[FK_tbl_Post_tbl_Blog]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [linkme_owner].[tbl_Post] DROP CONSTRAINT FK_tbl_Post_tbl_Blog
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Blog]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Blog]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Comment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Comment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_ItemTag]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_ItemTag]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Post]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Post]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Tags]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Tags]
GO

CREATE TABLE [linkme_owner].[tbl_Blog] (
	[id] [int] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Alias] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Description] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Security] [int] NOT NULL ,
	[Visibility] [int] NOT NULL ,
	[Count] [int] NOT NULL ,
	[External] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Publish] [bit] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [linkme_owner].[tbl_Comment] (
	[id] [int] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,
	[Parent] [int] NOT NULL ,
	[Comment] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Removed] [bit] NOT NULL ,
	[UserId] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Date] [datetime] NOT NULL ,
	[Type] [int] NOT NULL ,
	[Anonymous] [bit] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [linkme_owner].[tbl_ItemTag] (
	[id] [int] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,
	[Item] [int] NOT NULL ,
	[Tag] [int] NOT NULL ,
	[Type] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [linkme_owner].[tbl_Post] (
	[id] [int] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,
	[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Blog] [int] NULL ,
	[Content] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Date] [datetime] NOT NULL ,
	[Removed] [bit] NULL ,
	[Comments] [int] NULL ,
	[Published] [bit] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [linkme_owner].[tbl_Tags] (
	[id] [int] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,
	[Tag] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Count] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [linkme_owner].[tbl_Blog] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_Blogs] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [linkme_owner].[tbl_Comment] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_Comments] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [linkme_owner].[tbl_ItemTag] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_ItemTag] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [linkme_owner].[tbl_Post] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_Blog] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO



ALTER TABLE [linkme_owner].[tbl_Tags] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbl_Tags] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [linkme_owner].[tbl_Blog] WITH NOCHECK ADD 
	CONSTRAINT [DF_tbl_Blogs_Security] DEFAULT (1) FOR [Security],
	CONSTRAINT [DF_tbl_Blogs_Visibility] DEFAULT (1) FOR [Visibility],
	CONSTRAINT [DF_tbl_Blogs_Count] DEFAULT (0) FOR [Count],
	CONSTRAINT [DF_tbl_Blogs_Publish] DEFAULT (1) FOR [Publish]
GO

ALTER TABLE [linkme_owner].[tbl_Comment] WITH NOCHECK ADD 
	CONSTRAINT [DF_tbl_Comments_Removed] DEFAULT (0) FOR [Removed],
	CONSTRAINT [DF_tbl_Comments_Date] DEFAULT (getdate()) FOR [Date],
	CONSTRAINT [DF_tbl_Comment_Type] DEFAULT (0) FOR [Type],
	CONSTRAINT [DF_tbl_Comment_Annoymose] DEFAULT (1) FOR [Anonymous]
GO

ALTER TABLE [linkme_owner].[tbl_Post] WITH NOCHECK ADD 
	CONSTRAINT [DF_tbl_Blog_Date] DEFAULT (getdate()) FOR [Date],
	CONSTRAINT [DF_tbl_Blog_Removed] DEFAULT (0) FOR [Removed],
	CONSTRAINT [DF_tbl_Blog_Comments] DEFAULT (0) FOR [Comments],
	CONSTRAINT [DF_tbl_Post_Published] DEFAULT (0) FOR [Published]
GO


ALTER TABLE [linkme_owner].[tbl_Tags] WITH NOCHECK ADD 
	CONSTRAINT [DF_tbl_Tags_Count] DEFAULT (0) FOR [Count]
GO

ALTER TABLE [linkme_owner].[tbl_Post] ADD 
	CONSTRAINT [FK_tbl_Post_tbl_Blog] FOREIGN KEY 
	(
		[Blog]
	) REFERENCES [linkme_owner].[tbl_Blog] (
		[id]
	)
GO

