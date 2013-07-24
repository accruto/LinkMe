IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networker_resume_update'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	

create table linkme_owner.networker_resume_update
(
	id	varchar(100) PRIMARY KEY NOT NULL,
	batchDate	DateTime NULL,
	successCount	INTEGER NULL,
	errorCount	INTEGER NULL,
	startDateTime	DateTime NULL,
	endDateTime	DateTime NULL
	
)


CREATE NONCLUSTERED INDEX ix_networker_resume_update_batchDate ON linkme_owner.networker_resume_update (batchDate)	

END
GO
		