IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_profile_ideal_job'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE INDEX i_user_profile_ideal_job_employer ON linkme_owner.user_profile_ideal_job( currentEmployer ) ;
END

GO