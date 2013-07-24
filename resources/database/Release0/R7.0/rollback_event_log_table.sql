if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[event_log]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[event_log]
GO
