if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[search_log]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[search_log]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[search_log_result]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[search_log_result]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[search_log_criteria]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[search_log_criteria]
GO