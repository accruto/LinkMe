IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'Networker_overlooked_summary'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	DROP TABLE Networker_overlooked_summary

END
GO