IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'User_Profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	UPDATE linkme_owner.user_profile set optOutOfMonthlyEmails = 1 WHERE optOutOfMonthlyEmails  IS NULL
	
END
GO