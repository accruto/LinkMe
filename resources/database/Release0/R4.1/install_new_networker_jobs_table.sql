IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_jobs'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	CREATE TABLE networker_jobs
	(
		Id varchar(50),
		NetworkerId varchar(50) NULL,
		Employer varchar(100) NULL,
		Title varchar(100) NULL,
		JobStart varchar(50) NULL,
		JobEnd varchar(50) NULL
	)

	CREATE NONCLUSTERED INDEX i_networker_jobs_networkerId ON networker_jobs( NetworkerId )

END
GO