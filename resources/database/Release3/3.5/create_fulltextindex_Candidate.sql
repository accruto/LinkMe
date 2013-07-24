-- Enable full text indexing on the database.

if (select DATABASEPROPERTY(DB_NAME(), N'IsFullTextEnabled')) <> 1 
exec sp_fulltext_database N'enable' 
GO

-- Create the full text catalog.

if not exists (select * from dbo.sysfulltextcatalogs where name = N'CandidateCatalog')
exec sp_fulltext_catalog N'CandidateCatalog', N'create' 
GO

-- Drop the full text index on the table if it already exists.

if (ObjectProperty(object_id(N'dbo.Candidate'), 'TableFulltextCatalogId') <> 0)
exec sp_fulltext_table N'dbo.Candidate', N'drop'
GO

-- Create the full text index on the table.

exec sp_fulltext_table N'dbo.Candidate', N'create', N'CandidateCatalog', N'PK_Candidate'
GO

exec sp_fulltext_column N'dbo.Candidate', N'desiredJobTitle', N'add'

-- Configure the index to be automatically updated with any changes and activate it.

exec sp_fulltext_table N'dbo.Candidate', N'start_change_tracking'
GO
exec sp_fulltext_table N'dbo.Candidate', N'start_background_updateindex'
GO
exec sp_fulltext_table N'dbo.Candidate', N'activate'
GO
