
IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'employer_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	UPDATE employer_profile 
	SET invoiceAccountStatus = 'NotRequested'
	WHERE invoiceAccountStatus IS NULL OR invoiceAccountStatus = ''

END
GO