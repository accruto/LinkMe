IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'administrator_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	DROP TABLE linkme_owner.administrator_profile
END
GO