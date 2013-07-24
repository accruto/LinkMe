-- You actually have to "rebuild" a catalog before you can drop it!
EXEC sp_fulltext_catalog 'JobAdCatalog', 'Rebuild'
EXEC sp_fulltext_catalog 'EmployerProfileCatalog', 'Rebuild'

GO

EXEC sp_fulltext_table 'linkme_owner.employer_profile', 'Drop'
EXEC sp_fulltext_catalog 'EmployerProfileCatalog', 'Drop'

GO

EXEC sp_fulltext_table 'linkme_owner.JobAd', 'Drop'
EXEC sp_fulltext_catalog 'JobAdCatalog', 'Drop'

GO
