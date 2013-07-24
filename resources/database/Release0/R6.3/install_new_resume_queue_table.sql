IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'resume_queue'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.resume_queue
	(
		id	varchar(50) PRIMARY KEY NOT NULL,
		networkerId varchar(50) NULL,
		createdDate datetime NULL,
		lensQueueName varchar(50) NULL,
		status INT  NULL,
		submitDate datetime NULL,
		nonCriticalFailures int NULL,
		attempts int NULL,
		lockedBy varchar(50) NULL
	)	
END 
GO

