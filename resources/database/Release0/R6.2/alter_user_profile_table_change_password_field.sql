IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'User_Profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	ALTER TABLE linkme_owner.user_profile
	ADD changePasswordRequired BIT NULL
END
GO

IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'User_Profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	UPDATE linkme_owner.user_profile
	SET changePasswordRequired = 0
END
GO