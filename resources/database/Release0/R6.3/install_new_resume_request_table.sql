IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'resume_request'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.resume_request
	(
		id	varchar(50) PRIMARY KEY NOT NULL,
		networkerId varchar(50) NULL,
		resumeRequestSummaryId int NULL,
		status varchar(50) NULL,
		score varchar(50) NULL,
		yearsExperience varchar(50) NULL,
		networkerDeleted BIT NULL,
		employerDeleted BIT NULL
	)	
END 
GO

