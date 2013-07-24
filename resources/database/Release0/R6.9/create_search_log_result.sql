if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[search_log_result]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[search_log_result]
GO

CREATE TABLE [linkme_owner].[search_log_result] (
	[id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[searchLogId] [bigint] NOT NULL ,
	[networkerId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[rank] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [linkme_owner].[search_log_result] WITH NOCHECK ADD 
	CONSTRAINT [PK_search_log_results] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

