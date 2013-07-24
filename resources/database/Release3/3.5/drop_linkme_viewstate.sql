if exists (select * from dbo.sysobjects where id = object_id(N'dbo.linkme_viewstate') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table dbo.linkme_viewstate
GO
