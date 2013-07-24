-- Drop tables

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.tbl_Count') AND type in (N'U'))
DROP TABLE dbo.tbl_Count
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.tbl_Comment') AND type in (N'U'))
DROP TABLE dbo.tbl_Comment
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.tbl_Association') AND type in (N'U'))
DROP TABLE dbo.tbl_Association
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.tbl_Tag') AND type in (N'U'))
DROP TABLE dbo.tbl_Tag
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.tbl_Post') AND type in (N'U'))
DROP TABLE dbo.tbl_Post
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.tbl_Blog') AND type in (N'U'))
DROP TABLE dbo.tbl_Blog
GO



