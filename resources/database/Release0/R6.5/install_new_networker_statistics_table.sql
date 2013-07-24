IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_statistics'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.networker_statistics
	(
		id	varchar(50) NOT NULL,
		userProfileId varchar(50) NOT NULL,
		networkerInMatches INTEGER,
		NetworkerOutMatches INTEGER,
		EmployerMatches INTEGER,
		EmployerMisses INTEGER
		
		CONSTRAINT pk_networker_statistics_id PRIMARY KEY (id)
	)
	
	CREATE INDEX i_networker_statistics_userProfileId ON linkme_owner.networker_statistics (userProfileId)
END 
GO

