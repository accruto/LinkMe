IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'resume_request_summary'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

	ALTER TABLE linkme_owner.resume_request_summary
	ADD 
		salaryMinRate decimal(18,8) null,
		salaryMaxRate decimal(18,8) null,
		salaryPeriodType int null
END	
GO
IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'resume_request_summary'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	UPDATE linkme_owner.resume_request_summary 
	SET salaryPeriodType = 0 where salaryPeriod is null
	
	UPDATE linkme_owner.resume_request_summary 
	SET salaryPeriodType = 1 where salaryPeriod = 'Hour'

	UPDATE linkme_owner.resume_request_summary 
	SET salaryPeriodType = 2 where salaryPeriod = 'Day'

	UPDATE linkme_owner.resume_request_summary 
	SET salaryPeriodType = 3 where salaryPeriod = 'Week'

	UPDATE linkme_owner.resume_request_summary 
	SET salaryPeriodType = 4 where salaryPeriod = 'Month'

	UPDATE linkme_owner.resume_request_summary 
	SET salaryPeriodType = 5 where salaryPeriod = 'Year'


END	
GO
IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'resume_request_summary'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	ALTER TABLE linkme_owner.resume_request_summary
	ADD CONSTRAINT PK_resume_request_summary_id PRIMARY KEY ([id])
END
GO
