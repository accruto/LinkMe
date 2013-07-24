IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_jobs'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	DROP TABLE networker_jobs
END
GO