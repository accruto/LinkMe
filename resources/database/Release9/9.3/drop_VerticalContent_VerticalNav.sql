IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.VerticalContent') AND type in (N'U'))
DROP TABLE dbo.VerticalContent
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.VerticalNav') AND type in (N'U'))
DROP TABLE dbo.VerticalNav
GO

