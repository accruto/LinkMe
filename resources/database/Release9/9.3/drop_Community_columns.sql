IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Community]') AND name = N'UQ_Community_url')
ALTER TABLE [dbo].[Community] DROP CONSTRAINT [UQ_Community_url]
GO

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.Community') AND NAME = 'url')
BEGIN
	ALTER TABLE dbo.Community
	DROP COLUMN url
END
GO

IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.Community') AND NAME = 'host')
BEGIN
	ALTER TABLE dbo.Community
	DROP COLUMN host
END
GO
