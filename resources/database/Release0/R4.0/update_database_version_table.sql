IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'db_version'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.db_version (
		version VARCHAR(50)
	)
END

GO



IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'db_version'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	INSERT INTO linkme_owner.db_version VALUES ('4.0')
END

GO