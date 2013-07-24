IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'disabled_user_profile_info'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	

DROP table linkme_owner.disabled_user_profile_info


END
GO