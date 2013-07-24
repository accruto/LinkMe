if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[Photo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[Photo]
GO

CREATE TABLE [linkme_owner].[Photo] (
	[Id] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Photo] [image] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [linkme_owner].[Photo] WITH NOCHECK ADD 
	CONSTRAINT [PK_Photo] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO
