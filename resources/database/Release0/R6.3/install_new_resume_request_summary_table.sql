IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'resume_request_summary'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.resume_request_summary
	(
		id int IDENTITY (10000, 1) NOT NULL ,
		employerId varchar(50) NULL,
		expiryDate datetime NULL,
		status varchar(50) NULL,
		name varchar(200) NULL,
		summary TEXT NULL,
		receiveCandidateAcceptanceByEmail BIT NULL,
		salaryRange varchar(50) NULL,
		salaryPeriod varchar(50) NULL,
		companySize varchar(50) NULL,
		state varchar(20) NULL,
		workType varchar(50) NULL,
		location varchar(50) NULL,
		otherLocation varchar(200) NULL,
		industry varchar(50) NULL,
		jobCategory varchar(50) NULL,
		jobTitle varchar(200) NULL,
		deletedFlag BIT NULL,
		expiredClosedEmailSent BIT NULL
	)	
END 
GO

