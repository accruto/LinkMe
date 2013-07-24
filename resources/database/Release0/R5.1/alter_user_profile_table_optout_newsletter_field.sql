IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'User_Profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE linkme_owner.user_profile
	ADD OptOutOfNewsletters BIT NULL
	
END
GO