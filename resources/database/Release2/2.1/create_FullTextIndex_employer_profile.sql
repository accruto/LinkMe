-- Enable full text indexing on the database.

if (select DATABASEPROPERTY(DB_NAME(), N'IsFullTextEnabled')) <> 1 
exec sp_fulltext_database N'enable' 
GO

-- Create the full text catalog.

if not exists (select * from dbo.sysfulltextcatalogs where name = N'EmployerProfileCatalog')
exec sp_fulltext_catalog N'EmployerProfileCatalog', N'create' 
GO

-- Drop the full text index on the table if it already exists.

if (ObjectProperty(object_id(N'[linkme_owner].[employer_profile]'), 'TableFulltextCatalogId') <> 0)
exec sp_fulltext_table N'[linkme_owner].[employer_profile]', N'drop'
GO

-- Create the full text index on the table.

exec sp_fulltext_table N'[linkme_owner].[employer_profile]', N'create', N'EmployerProfileCatalog', N'PK_employer_profile'
GO

exec sp_fulltext_column N'[linkme_owner].[employer_profile]', N'organisationName', N'add'
GO

-- Configure the index to be automatically updated with any changes and activate it.

exec sp_fulltext_table N'[linkme_owner].[employer_profile]', N'start_change_tracking'
GO
exec sp_fulltext_table N'[linkme_owner].[employer_profile]', N'start_background_updateindex'
GO
exec sp_fulltext_table N'[linkme_owner].[employer_profile]', N'activate'
GO
