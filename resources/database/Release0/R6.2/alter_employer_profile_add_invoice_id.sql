IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'employer_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE linkme_owner.employer_profile
	ADD invoiceAccount BIT NULL

END
GO
