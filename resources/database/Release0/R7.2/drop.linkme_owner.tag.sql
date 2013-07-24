use [LinkMeTags]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbl_Association]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbl_Association]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbl_Count]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbl_Count]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbl_Tag]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbl_Tag]
GO
