IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_overlooked_summary'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	CREATE TABLE networker_overlooked_summary
	(
		id varchar(50),
		employerProfileId varchar(50)  NULL,
		purchaseComplete bit  NULL,
		searchDate DateTime  NULL,
		resultsKey varchar(50) NULL
	)

	CREATE NONCLUSTERED INDEX ix_networker_overlooked_summary_employer_profile_id ON networker_overlooked_summary(employerProfileId );
	

END
GO