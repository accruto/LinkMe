-- Enable full text indexing on the database.

if (select DATABASEPROPERTY(DB_NAME(), N'IsFullTextEnabled')) <> 1 
exec sp_fulltext_database N'enable' 
GO

-- Create the full text catalog.

if not exists (select * from dbo.sysfulltextcatalogs where name = N'JobAdCatalog')
exec sp_fulltext_catalog N'JobAdCatalog', N'create' 
GO

-- Drop the full text index on the table if it already exists.

if (ObjectProperty(object_id(N'dbo.[JobAd]'), 'TableFulltextCatalogId') <> 0)
exec sp_fulltext_table N'dbo.[JobAd]', N'drop'
GO

-- Create the full text index on the table.

exec sp_fulltext_table N'dbo.[JobAd]', N'create', N'JobAdCatalog', N'PK_JobAd'
GO

exec sp_fulltext_column N'dbo.[JobAd]', N'bulletPoints', N'add'
GO
exec sp_fulltext_column N'dbo.[JobAd]', N'content', N'add'
GO
exec sp_fulltext_column N'dbo.[JobAd]', N'employerCompanyName', N'add'
GO
exec sp_fulltext_column N'dbo.[JobAd]', N'externalReferenceId', N'add'
GO
exec sp_fulltext_column N'dbo.[JobAd]', N'packageDetails', N'add'
GO
exec sp_fulltext_column N'dbo.[JobAd]', N'title', N'add'
GO

-- Configure the index to be automatically updated with any changes and activate it.

exec sp_fulltext_table N'dbo.[JobAd]', N'start_change_tracking'
GO
exec sp_fulltext_table N'dbo.[JobAd]', N'start_background_updateindex'
GO
exec sp_fulltext_table N'dbo.[JobAd]', N'activate'
GO
