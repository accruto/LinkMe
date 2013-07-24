IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_data'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE user_data
	DROP COLUMN textValue;

END
GO