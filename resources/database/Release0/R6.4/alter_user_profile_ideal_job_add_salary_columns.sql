IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_profile_ideal_job'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

	ALTER TABLE linkme_owner.user_profile_ideal_job
	ADD 
		currentSalaryMinRate 	decimal(18,8)	NULL,
		currentSalaryMaxRate 	decimal(18,8)	NULL,
		currentSalaryRateType	varchar(15)		NULL,
		idealSalaryMinRate 	decimal(18,8)		NULL,
		idealSalaryMaxRate 	decimal(18,8)		NULL,
		idealSalaryRateType	varchar(15)		NULL

END
GO
