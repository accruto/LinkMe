IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'disabled_user_profile_info'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	



create table linkme_owner.disabled_user_profile_info
(
	id VARCHAR(50) PRIMARY KEY NOT NULL,
	userProfileUserId VARCHAR(150) NOT NULL,
	DisabledDate DATETIME NOT NULL
)



END
GO

