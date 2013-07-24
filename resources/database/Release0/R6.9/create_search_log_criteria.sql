if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[search_log_criteria]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[search_log_criteria]
GO

CREATE TABLE [linkme_owner].[search_log_criteria] (
	[id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[searchLogId] [bigint] NOT NULL ,
	[criteriaType] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[criteriaValue] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

ALTER TABLE [linkme_owner].[search_log_criteria] WITH NOCHECK ADD 
	CONSTRAINT [PK_search_log_criteria] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

