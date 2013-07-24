IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'resume_request_summary'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

	ALTER TABLE linkme_owner.resume_request_summary
	DROP COLUMN
		salaryMinRate,
		salaryMaxRate,
		salaryPeriodType

END
GO
