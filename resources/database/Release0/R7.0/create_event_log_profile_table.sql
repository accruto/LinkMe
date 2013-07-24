if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[event_log_profile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[event_log_profile]
GO

CREATE TABLE [linkme_owner].[event_log_profile] (
	[id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[eventLogId] [bigint] NOT NULL ,
	[profileId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [linkme_owner].[event_log_profile] WITH NOCHECK ADD 
	CONSTRAINT [PK_event_log_profile] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

