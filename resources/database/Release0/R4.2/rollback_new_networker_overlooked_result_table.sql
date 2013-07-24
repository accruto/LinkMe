IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_overlooked_result'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	DROP TABLE networker_overlooked_result

END
GO