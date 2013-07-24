if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[search_log]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[search_log]
GO

CREATE TABLE [linkme_owner].[search_log] (
	[id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[profileId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[runDate] [datetime] NOT NULL ,
	[resultCount] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [linkme_owner].[search_log] WITH NOCHECK ADD 
	CONSTRAINT [PK_search_log] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [linkme_owner].[search_log] WITH NOCHECK ADD 
	CONSTRAINT [DF_search_log_dateRun] DEFAULT (getdate()) FOR [runDate]
GO

