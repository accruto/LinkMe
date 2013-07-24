IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'resume_request_summary'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

DROP table linkme_owner.resume_request_summary
	
END 
GO

