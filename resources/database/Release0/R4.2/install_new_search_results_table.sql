IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'search_result'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	CREATE TABLE search_result
	(
		id varchar(50),
		savedSearchId varchar(50) NULL,
		networkerId varchar(50) NULL,
		yearsExperience integer NULL,
		score integer NULL,
		endorsementsCount integer NULL
	)

	CREATE NONCLUSTERED INDEX i_search_results_id ON search_result(savedSearchId )

END
GO