if exists (select * from dbo.sysobjects where id = object_id(N'[locality]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [locality]
GO


