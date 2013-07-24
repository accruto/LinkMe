IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'searches'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE SEARCHES
	DROP COLUMN lastRunDate

	ALTER TABLE SEARCHES
	DROP COLUMN lastViewedDate 

	ALTER TABLE SEARCHES
	DROP COLUMN minimumScore 

	ALTER TABLE SEARCHES
	DROP COLUMN status 

	
END
GO