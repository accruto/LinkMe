if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Association]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Association]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Count]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Count]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Tag]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Tag]
GO