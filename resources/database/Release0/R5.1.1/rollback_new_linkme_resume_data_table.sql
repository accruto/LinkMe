IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_resume_data'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	

	DROP TABLE linkme_owner.networker_resume_data

END
GO