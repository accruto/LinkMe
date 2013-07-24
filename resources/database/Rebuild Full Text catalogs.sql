EXEC sp_fulltext_database 'enable'

GO

EXEC sp_fulltext_catalog 'JobAdCatalog', 'Rebuild'
EXEC sp_fulltext_catalog 'JobAdCatalog', 'start_full'

GO

EXEC sp_fulltext_catalog 'CompanyCatalog', 'Rebuild'
EXEC sp_fulltext_catalog 'CompanyCatalog', 'start_full'

GO

EXEC sp_fulltext_catalog 'CandidateCatalog', 'Rebuild'
EXEC sp_fulltext_catalog 'CandidateCatalog', 'start_full'

GO
