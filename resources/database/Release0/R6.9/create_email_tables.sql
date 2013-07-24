if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[mail_merge_content_item]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[mail_merge_content_item]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[mail_merge_task]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[mail_merge_task]
GO

CREATE TABLE [linkme_owner].[mail_merge_content_item] (
	[id] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[subjecttext] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[templatebody] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[mimetype] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[localecode] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[taskid] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [linkme_owner].[mail_merge_task] (
	[id] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[taskname] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[searchclassname] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [linkme_owner].[mail_merge_content_item] WITH NOCHECK ADD 
	CONSTRAINT [PK_mail_merge_content_item] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [linkme_owner].[mail_merge_task] WITH NOCHECK ADD 
	CONSTRAINT [PK_mail_merge_task] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO