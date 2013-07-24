IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_jobs'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE linkme_owner.networker_jobs 
	DROP CONSTRAINT networker_jobs_primary_key

END
GO
