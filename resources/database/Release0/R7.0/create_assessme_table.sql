if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[networker_assessment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[networker_assessment]
GO


CREATE TABLE [linkme_owner].[networker_assessment] (
	[id] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[assessmeId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[reportlink] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[status] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[type] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[created] [datetime] NULL ,
	[networkerId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO


ALTER TABLE [linkme_owner].[networker_assessment] WITH NOCHECK ADD 
	CONSTRAINT [PK_networker_assessment] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO
