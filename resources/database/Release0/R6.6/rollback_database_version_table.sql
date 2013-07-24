IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'db_version'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	DELETE FROM linkme_owner.db_version WHERE version='6.6' 
END

GO
