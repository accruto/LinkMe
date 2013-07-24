IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'searches'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	CREATE TABLE searches
	(
		id varchar(50),
		searchName varchar(50) NULL,
		profileId varchar(50) NULL
	)

	CREATE NONCLUSTERED INDEX i_searches_profile_id ON searches( profileId )

END
GO