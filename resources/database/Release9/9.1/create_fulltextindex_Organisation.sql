-- Enable full text indexing on the database.

if (select DATABASEPROPERTY(DB_NAME(), N'IsFullTextEnabled')) <> 1 
exec sp_fulltext_database N'enable' 
GO

-- Create the full text catalog.

if not exists (select * from dbo.sysfulltextcatalogs where name = N'OrganisationCatalog')
exec sp_fulltext_catalog N'OrganisationCatalog', N'create' 
GO

-- Drop the full text index on the table if it already exists.

if (ObjectProperty(object_id(N'dbo.Organisation'), 'TableFulltextCatalogId') <> 0)
exec sp_fulltext_table N'dbo.Organisation', N'drop'
GO

-- Create the full text index on the table.

exec sp_fulltext_table N'dbo.Organisation', N'create', N'OrganisationCatalog', N'PK_Organisation'
GO

exec sp_fulltext_column N'dbo.Organisation', N'displayName', N'add'

-- Configure the index to be automatically updated with any changes and activate it.

exec sp_fulltext_table N'dbo.Organisation', N'start_change_tracking'
GO
exec sp_fulltext_table N'dbo.Organisation', N'start_background_updateindex'
GO
exec sp_fulltext_table N'dbo.Organisation', N'activate'
GO
