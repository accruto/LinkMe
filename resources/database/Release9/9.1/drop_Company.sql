-- It seems counter-intuitive, but the indexed table needs to be dropped
-- first, BEFORE the full-text index.

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
DROP TABLE [dbo].Company
GO

IF  EXISTS (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'CompanyCatalog')
DROP FULLTEXT CATALOG [CompanyCatalog]
GO
