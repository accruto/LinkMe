IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'search_criteria'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	CREATE TABLE search_criteria
	(
		id varchar(50),
		criteriaType varchar(50) NULL,
		value TEXT NULL
	)

	CREATE NONCLUSTERED INDEX i_searches_criteria_id ON search_criteria( criteriaType)

END
GO