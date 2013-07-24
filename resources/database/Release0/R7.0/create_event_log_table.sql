if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[event_log]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[event_log]
GO

CREATE TABLE [linkme_owner].[event_log] (
	[id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[profileId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[date] [datetime] NOT NULL,
	[event] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[context] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS 
) ON [PRIMARY]
GO

ALTER TABLE [linkme_owner].[event_log] WITH NOCHECK ADD 
	CONSTRAINT [PK_event_log] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [linkme_owner].[event_log] WITH NOCHECK ADD 
	CONSTRAINT [DF_event_log_date] DEFAULT (getdate()) FOR [date]
GO

