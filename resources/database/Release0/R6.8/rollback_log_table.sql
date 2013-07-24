if exists (select * from dbo.sysobjects where id = object_id(N'[Log]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Log]
GO


