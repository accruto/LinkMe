IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	ALTER TABLE linkme_owner.networker_profile DROP COLUMN matchCompanySearches;
	ALTER TABLE linkme_owner.networker_profile DROP COLUMN matchCurrentEmployerSearches;
	ALTER TABLE linkme_owner.networker_profile DROP COLUMN matchContactNameSearches;
END
GO