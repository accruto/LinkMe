IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_purchased_result'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	ALTER TABLE networker_purchased_result
	DROP COLUMN rank
	
END
GO