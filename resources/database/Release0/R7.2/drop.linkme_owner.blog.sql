if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[FK_tbl_Post_tbl_Blog]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [linkme_owner].[tbl_Post] DROP CONSTRAINT FK_tbl_Post_tbl_Blog
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Blog]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Blog]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Comment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Comment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_ItemTag]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_ItemTag]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Post]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Post]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[tbl_Tags]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[tbl_Tags]
GO
