IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_account'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	INSERT INTO linkme_owner.user_account
	SELECT ID, NULL, '4', ''
	FROM linkme_owner.user_profile

END
GO