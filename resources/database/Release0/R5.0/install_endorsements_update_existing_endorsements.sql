IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'endorsement'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	UPDATE ENDORSEMENT SET endorsementDate = GETDATE() WHERE endorsementDate = NULL
	
END
GO